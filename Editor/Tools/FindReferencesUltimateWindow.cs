using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    /// <summary>
    /// Find asset references via:
    /// 1) AssetDatabase dependency graph (fast)
    /// 2) GUID scan in YAML/text (stronger) - streaming + cache
    /// </summary>
    public sealed class FindReferencesUltimateWindow : EditorWindow
    {
        // ---------------- UI ----------------
        private Object _target;
        private DefaultAsset _searchRoot; // folder in project
        private bool _onlyUnderAssets = true;

        private bool _useDependencyGraph = true;
        private bool _useGuidScan = true;

        private bool _includePrefabs = true;
        private bool _includeScenes = true;
        private bool _includeScriptableObjects = true;
        private bool _includeMaterials = true;
        private bool _includeAnimatorControllers = true;
        private bool _includeOtherYamlAssets = false;
        private bool _includeTextFiles = false;

        private string _nameFilter = "";
        private string _excludeFolderPatterns = "Assets/ThirdParty\nAssets/AddressableAssetsData";
        private bool _useRegexForExclude = false;

        private bool _groupByType = true;
        private bool _showPaths = true;
        private bool _autoSelect = true;

        private int _maxFileSizeMb = 50;
        private int _streamChunkSizeKb = 64;

        private bool _exportIncludeHeader = true;

        // ---------------- Results ----------------
        private readonly List<ResultItem> _results = new List<ResultItem>(256);
        private readonly Dictionary<string, ResultItem> _resultMap = new Dictionary<string, ResultItem>(512, StringComparer.Ordinal);
        private Vector2 _scroll;

        // ---------------- Scan State ----------------
        private bool _isScanning;
        private string _status;
        private float _progress01;

        // step driver
        private ScanState _scanState;
        private const int WorkBudgetPerEditorUpdate = 200;

        // ---------------- Cache ----------------
        // Cache for GUID scan: filePath -> (lastWriteUtc + length + containsGuid)
        private readonly Dictionary<string, FileCache> _guidCache = new Dictionary<string, FileCache>(4096, StringComparer.Ordinal);

        private struct FileCache
        {
            public long Length;
            public long LastWriteTicksUtc;
            public bool Contains;
        }

        private struct ResultItem
        {
            public string Path;
            public string TypeName;
            public Object Asset;
        }

        [MenuItem("Tools/UP-Common/Assets/Find References (Ultimate)")]
        private static void Open()
        {
            var w = GetWindow<FindReferencesUltimateWindow>("Find References");
            w.minSize = new Vector2(560, 520);
        }

        private void OnEnable()
        {
            if (_searchRoot == null)
                _searchRoot = AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets");

            _status = "Idle.";
        }

        private void OnDisable()
        {
            StopScan();
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawConfig();
            DrawActions();
            DrawResults();
        }

        private void DrawHeader()
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("Find References (Ultimate)", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Select a target asset, then scan the project for assets referencing it.\n" +
                "• Dependency Graph: fast\n" +
                "• GUID Scan: stronger (YAML/text), uses streaming + cache\n" +
                "Tip: Use Search Root + Exclude patterns to speed up.",
                MessageType.Info);
        }

        private void DrawConfig()
        {
            EditorGUILayout.Space(6);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                _target = EditorGUILayout.ObjectField("Target", _target, typeof(Object), false);

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.LabelField("Search Root", GUILayout.Width(90));
                    _searchRoot = (DefaultAsset)EditorGUILayout.ObjectField(_searchRoot, typeof(DefaultAsset), false);

                    if (GUILayout.Button("Use Assets/", GUILayout.Width(110)))
                        _searchRoot = AssetDatabase.LoadAssetAtPath<DefaultAsset>("Assets");
                }

                _onlyUnderAssets = EditorGUILayout.ToggleLeft("Only search under Assets/", _onlyUnderAssets);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Mode", EditorStyles.boldLabel);
                _useDependencyGraph = EditorGUILayout.ToggleLeft("Dependency Graph (fast)", _useDependencyGraph);
                _useGuidScan = EditorGUILayout.ToggleLeft("GUID Scan (YAML/Text) (stronger)", _useGuidScan);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Asset Types", EditorStyles.boldLabel);
                _includePrefabs = EditorGUILayout.ToggleLeft("Prefabs (.prefab)", _includePrefabs);
                _includeScenes = EditorGUILayout.ToggleLeft("Scenes (.unity)", _includeScenes);
                _includeScriptableObjects = EditorGUILayout.ToggleLeft("ScriptableObjects (t:ScriptableObject)", _includeScriptableObjects);
                _includeMaterials = EditorGUILayout.ToggleLeft("Materials (.mat)", _includeMaterials);
                _includeAnimatorControllers = EditorGUILayout.ToggleLeft("AnimatorControllers (.controller)", _includeAnimatorControllers);
                _includeOtherYamlAssets = EditorGUILayout.ToggleLeft("Other YAML assets (.asset etc.)", _includeOtherYamlAssets);
                _includeTextFiles = EditorGUILayout.ToggleLeft("Text/JSON/YAML files", _includeTextFiles);

                EditorGUILayout.Space(4);
                _nameFilter = EditorGUILayout.TextField("Name filter (contains)", _nameFilter);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Exclude Folders", EditorStyles.boldLabel);
                _useRegexForExclude = EditorGUILayout.ToggleLeft("Use Regex", _useRegexForExclude);
                EditorGUILayout.LabelField("One pattern per line (folder path prefix).", EditorStyles.miniLabel);
                _excludeFolderPatterns = EditorGUILayout.TextArea(_excludeFolderPatterns, GUILayout.Height(50));

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Performance", EditorStyles.boldLabel);
                _maxFileSizeMb = EditorGUILayout.IntSlider("Max file size for GUID scan (MB)", _maxFileSizeMb, 1, 200);
                _streamChunkSizeKb = EditorGUILayout.IntSlider("Stream chunk size (KB)", _streamChunkSizeKb, 8, 512);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Display", EditorStyles.boldLabel);
                _groupByType = EditorGUILayout.ToggleLeft("Group by type", _groupByType);
                _showPaths = EditorGUILayout.ToggleLeft("Show paths", _showPaths);
                _autoSelect = EditorGUILayout.ToggleLeft("Auto select results", _autoSelect);

                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Export", EditorStyles.boldLabel);
                _exportIncludeHeader = EditorGUILayout.ToggleLeft("Include header in export", _exportIncludeHeader);
            }
        }

        private void DrawActions()
        {
            EditorGUILayout.Space(6);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Use Selection", GUILayout.Height(26)))
                {
                    _target = Selection.activeObject;
                    Repaint();
                }

                using (new EditorGUI.DisabledScope(_isScanning))
                {
                    if (GUILayout.Button("Scan", GUILayout.Height(26)))
                        StartScan();
                }

                using (new EditorGUI.DisabledScope(!_isScanning))
                {
                    if (GUILayout.Button("Cancel", GUILayout.Height(26)))
                        StopScan();
                }
            }

            if (_isScanning)
            {
                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField(_status);

                Rect r = GUILayoutUtility.GetRect(10, 18);
                EditorGUI.ProgressBar(r, _progress01, $"{Mathf.RoundToInt(_progress01 * 100f)}%");
            }
            else
            {
                if (_results.Count > 0)
                {
                    EditorGUILayout.Space(4);
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("Select All"))
                            SelectAllResults();

                        if (GUILayout.Button("Ping First"))
                            PingFirst();

                        if (GUILayout.Button("Copy Paths"))
                            CopyPathsToClipboard();

                        if (GUILayout.Button("Export To Assets/ (txt)"))
                            ExportToAssetsTxt();

                        if (GUILayout.Button("Clear"))
                            ClearResults();
                    }
                }
            }
        }

        private void DrawResults()
        {
            EditorGUILayout.Space(6);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField($"Results: {_results.Count}", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Tip: Double-click a row to open asset.", EditorStyles.miniLabel);

                if (_results.Count == 0)
                {
                    EditorGUILayout.LabelField("No results.");
                    return;
                }

                _scroll = EditorGUILayout.BeginScrollView(_scroll);

                if (_groupByType)
                    DrawGroupedResults();
                else
                    DrawFlatResults();

                EditorGUILayout.EndScrollView();
            }
        }

        private void DrawGroupedResults()
        {
            var order = new[] { "SceneAsset", "GameObject", "ScriptableObject", "Material", "AnimatorController", "Other" };
            var groups = new Dictionary<string, List<ResultItem>>(16, StringComparer.Ordinal);

            for (int i = 0; i < _results.Count; i++)
            {
                var item = _results[i];
                if (!groups.TryGetValue(item.TypeName, out var list))
                {
                    list = new List<ResultItem>(64);
                    groups[item.TypeName] = list;
                }
                list.Add(item);
            }

            for (int i = 0; i < order.Length; i++)
            {
                if (!groups.TryGetValue(order[i], out var list) || list.Count == 0) continue;
                DrawGroup(order[i], list);
            }

            foreach (var kv in groups)
            {
                bool exists = false;
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] == kv.Key) { exists = true; break; }
                }
                if (exists) continue;
                DrawGroup(kv.Key, kv.Value);
            }
        }

        private void DrawGroup(string title, List<ResultItem> list)
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField($"{title} ({list.Count})", EditorStyles.boldLabel);

            for (int i = 0; i < list.Count; i++)
                DrawRow(list[i]);
        }

        private void DrawFlatResults()
        {
            for (int i = 0; i < _results.Count; i++)
                DrawRow(_results[i]);
        }

        private void DrawRow(ResultItem item)
        {
            // draw clickable row
            Rect row = EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Ping", GUILayout.Width(44)))
                {
                    if (item.Asset != null)
                    {
                        EditorGUIUtility.PingObject(item.Asset);
                        Selection.activeObject = item.Asset;
                    }
                }

                if (GUILayout.Button("Open", GUILayout.Width(44)))
                {
                    if (item.Asset != null)
                        AssetDatabase.OpenAsset(item.Asset);
                }

                GUILayout.Label(item.Asset != null ? item.Asset.name : "(Missing)", GUILayout.Width(220));
                GUILayout.Label(item.TypeName, GUILayout.Width(140));

                if (_showPaths)
                    GUILayout.Label(item.Path);
            }
            EditorGUILayout.EndHorizontal();

            // Double-click anywhere on row (except buttons) to open asset
            HandleRowDoubleClick(row, item);
        }

        private void HandleRowDoubleClick(Rect rowRect, ResultItem item)
        {
            var e = Event.current;
            if (e == null) return;

            // ignore if click on buttons area (left side)
            // buttons occupy about 90 px width
            var safeRect = rowRect;
            safeRect.xMin += 95f;

            if (e.type == EventType.MouseDown && e.button == 0 && e.clickCount == 2 && safeRect.Contains(e.mousePosition))
            {
                if (item.Asset != null)
                {
                    Selection.activeObject = item.Asset;
                    EditorGUIUtility.PingObject(item.Asset);
                    AssetDatabase.OpenAsset(item.Asset);
                    e.Use();
                }
            }
        }

        // ---------------- Actions ----------------
        private void StartScan()
        {
            if (_isScanning) return;

            if (_target == null)
                _target = Selection.activeObject;

            if (_target == null)
            {
                Debug.LogWarning("No target selected.");
                return;
            }

            string targetPath = AssetDatabase.GetAssetPath(_target);
            if (string.IsNullOrEmpty(targetPath) || AssetDatabase.IsValidFolder(targetPath))
            {
                Debug.LogWarning("Target must be a non-folder asset.");
                return;
            }

            if (!_useDependencyGraph && !_useGuidScan)
            {
                Debug.LogWarning("Enable at least one mode.");
                return;
            }

            string root = GetRootFolder();
            if (string.IsNullOrEmpty(root))
            {
                Debug.LogWarning("Search root must be a valid folder.");
                return;
            }

            string guid = AssetDatabase.AssetPathToGUID(targetPath);
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogWarning("Cannot get GUID of target.");
                return;
            }

            ClearResults();

            var excludes = BuildExcludeMatchers(_excludeFolderPatterns, _useRegexForExclude);

            _scanState = new ScanState(
                targetPath,
                guid,
                root,
                _onlyUnderAssets,
                _useDependencyGraph,
                _useGuidScan,
                _nameFilter,
                excludes,
                BuildDependencyFilters(),
                BuildGuidScanExtensions(_includeTextFiles, _includeOtherYamlAssets),
                _maxFileSizeMb,
                _streamChunkSizeKb * 1024,
                _guidCache
            );

            _isScanning = true;
            _status = "Starting...";
            _progress01 = 0f;

            EditorApplication.update += OnEditorUpdateScan;
        }

        private void StopScan()
        {
            if (!_isScanning) return;

            _isScanning = false;
            _status = "Canceled.";
            _progress01 = 0f;

            EditorApplication.update -= OnEditorUpdateScan;
            _scanState = null;

            Repaint();
        }

        private void OnEditorUpdateScan()
        {
            if (!_isScanning || _scanState == null)
            {
                EditorApplication.update -= OnEditorUpdateScan;
                return;
            }

            int worked = 0;

            try
            {
                while (worked < WorkBudgetPerEditorUpdate)
                {
                    if (!_scanState.Next(out var ev))
                    {
                        FinishScan();
                        return;
                    }

                    worked++;
                    _status = ev.Status;
                    _progress01 = ev.Progress01;

                    if (!string.IsNullOrEmpty(ev.FoundPath))
                        AddResult(ev.FoundPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                FinishScan();
            }

            Repaint();
        }

        private void FinishScan()
        {
            _isScanning = false;
            _status = $"Done. {_results.Count} results.";
            _progress01 = 1f;

            EditorApplication.update -= OnEditorUpdateScan;
            _scanState = null;

            if (_autoSelect && _results.Count > 0)
                SelectAllResults();

            Repaint();
        }

        private void AddResult(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            if (_resultMap.ContainsKey(path)) return;

            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            if (asset == null) return;

            var item = new ResultItem
            {
                Path = path,
                Asset = asset,
                TypeName = GetTypeName(asset)
            };

            _resultMap[path] = item;
            _results.Add(item);
        }

        private static string GetTypeName(Object obj)
        {
            if (obj is SceneAsset) return "SceneAsset";
            if (obj is GameObject) return "GameObject";
            if (obj is Material) return "Material";
            var t = obj.GetType();
            if (t.FullName == "UnityEditor.Animations.AnimatorController") return "AnimatorController";
            if (obj is ScriptableObject) return "ScriptableObject";
            return "Other";
        }

        private void SelectAllResults()
        {
            var list = new List<Object>(_results.Count);
            for (int i = 0; i < _results.Count; i++)
                if (_results[i].Asset != null) list.Add(_results[i].Asset);

            Selection.objects = list.ToArray();
        }

        private void PingFirst()
        {
            if (_results.Count == 0) return;
            var obj = _results[0].Asset;
            if (obj == null) return;

            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }

        private void CopyPathsToClipboard()
        {
            var sb = new StringBuilder(_results.Count * 32);
            for (int i = 0; i < _results.Count; i++)
                sb.AppendLine(_results[i].Path);

            EditorGUIUtility.systemCopyBuffer = sb.ToString();
            Debug.Log("Paths copied to clipboard.");
        }

        private void ExportToAssetsTxt()
        {
            string fileName = $"UPCommon_FindRefs_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string path = Path.Combine(Application.dataPath, fileName);

            var sb = new StringBuilder(_results.Count * 64);

            if (_exportIncludeHeader)
            {
                sb.AppendLine("UP-Common - Find References Export");
                sb.AppendLine($"Target: {(_target != null ? _target.name : "(null)")}");
                sb.AppendLine($"Time: {DateTime.Now}");
                sb.AppendLine($"Results: {_results.Count}");
                sb.AppendLine(new string('-', 60));
            }

            for (int i = 0; i < _results.Count; i++)
                sb.AppendLine(_results[i].Path);

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh();

            Debug.Log($"Exported results to: Assets/{fileName}");
        }

        private void ClearResults()
        {
            _results.Clear();
            _resultMap.Clear();
        }

        // ---------------- Helpers ----------------
        private string GetRootFolder()
        {
            if (_searchRoot == null)
                return _onlyUnderAssets ? "Assets" : "Assets";

            string path = AssetDatabase.GetAssetPath(_searchRoot);
            if (string.IsNullOrEmpty(path) || !AssetDatabase.IsValidFolder(path))
                return _onlyUnderAssets ? "Assets" : "Assets";

            if (_onlyUnderAssets && !path.StartsWith("Assets", StringComparison.Ordinal))
                return "Assets";

            return path;
        }

        private List<string> BuildDependencyFilters()
        {
            var list = new List<string>(8);
            if (_includePrefabs) list.Add("t:Prefab");
            if (_includeScenes) list.Add("t:Scene");
            if (_includeScriptableObjects) list.Add("t:ScriptableObject");
            if (_includeMaterials) list.Add("t:Material");
            if (_includeAnimatorControllers) list.Add("t:AnimatorController");
            if (list.Count == 0) list.Add("t:Prefab");
            return list;
        }

        private HashSet<string> BuildGuidScanExtensions(bool includeText, bool includeOtherYaml)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (_includePrefabs) set.Add(".prefab");
            if (_includeScenes) set.Add(".unity");
            if (_includeMaterials) set.Add(".mat");
            if (_includeAnimatorControllers) set.Add(".controller");

            if (_includeScriptableObjects || includeOtherYaml)
            {
                set.Add(".asset");
                set.Add(".overridecontroller");
                set.Add(".playable");
                set.Add(".rendertexture");
                set.Add(".shadervariants");
            }

            if (includeText)
            {
                set.Add(".json");
                set.Add(".txt");
                set.Add(".bytes");
                set.Add(".xml");
                set.Add(".yaml");
                set.Add(".yml");
            }

            return set;
        }

        private static List<IExcludeMatcher> BuildExcludeMatchers(string patternsText, bool useRegex)
        {
            var list = new List<IExcludeMatcher>(16);
            if (string.IsNullOrWhiteSpace(patternsText)) return list;

            var lines = patternsText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string p = lines[i].Trim();
                if (string.IsNullOrEmpty(p)) continue;

                if (useRegex)
                {
                    try { list.Add(new RegexExcludeMatcher(p)); }
                    catch { /* ignore invalid regex */ }
                }
                else
                {
                    // prefix path match
                    list.Add(new PrefixExcludeMatcher(p.Replace('\\', '/')));
                }
            }

            return list;
        }

        // ---------------- Scan Engine ----------------
        private sealed class ScanState
        {
            private readonly string _targetPath;
            private readonly string _targetGuid;
            private readonly string _rootFolder;
            private readonly bool _onlyUnderAssets;

            private readonly bool _doDeps;
            private readonly bool _doGuid;

            private readonly string _nameFilterLower;
            private readonly List<IExcludeMatcher> _excludes;

            private readonly List<string> _depFilters;
            private readonly HashSet<string> _scanExts;

            private readonly int _maxFileBytes;
            private readonly int _chunkBytes;

            private readonly Dictionary<string, FileCache> _cache;

            // dep state
            private int _depFilterIndex;
            private string[] _currentDepGuids;
            private int _currentDepIndex;

            // file state
            private string[] _files;
            private int _fileIndex;

            public ScanState(
                string targetPath,
                string targetGuid,
                string rootFolder,
                bool onlyUnderAssets,
                bool doDeps,
                bool doGuid,
                string nameFilter,
                List<IExcludeMatcher> excludes,
                List<string> depFilters,
                HashSet<string> scanExts,
                int maxFileSizeMb,
                int chunkBytes,
                Dictionary<string, FileCache> cache)
            {
                _targetPath = targetPath;
                _targetGuid = targetGuid;
                _rootFolder = rootFolder;
                _onlyUnderAssets = onlyUnderAssets;

                _doDeps = doDeps;
                _doGuid = doGuid;

                _nameFilterLower = string.IsNullOrWhiteSpace(nameFilter) ? "" : nameFilter.Trim().ToLowerInvariant();
                _excludes = excludes ?? new List<IExcludeMatcher>(0);

                _depFilters = depFilters ?? new List<string>(0);
                _scanExts = scanExts ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                _maxFileBytes = Mathf.Clamp(maxFileSizeMb, 1, 500) * 1024 * 1024;
                _chunkBytes = Mathf.Clamp(chunkBytes, 8 * 1024, 1024 * 1024);

                _cache = cache ?? new Dictionary<string, FileCache>(1024, StringComparer.Ordinal);
            }

            public bool Next(out ScanEvent ev)
            {
                // dependency first
                if (_doDeps)
                {
                    if (NextDependency(out ev))
                        return true;
                }

                if (_doGuid)
                {
                    if (NextGuidScan(out ev))
                        return true;
                }

                ev = default;
                return false;
            }

            private bool NextDependency(out ScanEvent ev)
            {
                if (_depFilters.Count == 0)
                {
                    ev = default;
                    return false;
                }

                if (_currentDepGuids == null)
                {
                    if (_depFilterIndex >= _depFilters.Count)
                    {
                        ev = default;
                        return false;
                    }

                    string filter = _depFilters[_depFilterIndex++];
                    _currentDepGuids = AssetDatabase.FindAssets(filter, new[] { _rootFolder });
                    _currentDepIndex = 0;

                    ev = new ScanEvent
                    {
                        Status = $"Dependency scan: {filter}",
                        Progress01 = 0.02f
                    };
                    return true;
                }

                if (_currentDepIndex >= _currentDepGuids.Length)
                {
                    _currentDepGuids = null;
                    ev = new ScanEvent { Status = "Dependency scan: next group...", Progress01 = 0.30f };
                    return true;
                }

                string guid = _currentDepGuids[_currentDepIndex++];
                string path = AssetDatabase.GUIDToAssetPath(guid);

                ev = new ScanEvent
                {
                    Status = $"Dependency scan: {_currentDepIndex}/{_currentDepGuids.Length}",
                    Progress01 = Mathf.Clamp01(0.03f + (_depFilterIndex / (float)Mathf.Max(1, _depFilters.Count)) * 0.35f)
                };

                if (string.IsNullOrEmpty(path) || path == _targetPath) return true;

                if (!PassCommonFilters(path)) return true;

                var deps = AssetDatabase.GetDependencies(path, true);
                for (int i = 0; i < deps.Length; i++)
                {
                    if (deps[i] == _targetPath)
                    {
                        ev.FoundPath = path;
                        break;
                    }
                }

                return true;
            }

            private bool NextGuidScan(out ScanEvent ev)
            {
                if (_scanExts.Count == 0)
                {
                    ev = default;
                    return false;
                }

                if (_files == null)
                {
                    string rootFull = Path.GetFullPath(_rootFolder);
                    if (!Directory.Exists(rootFull))
                    {
                        ev = default;
                        return false;
                    }

                    _files = Directory.GetFiles(rootFull, "*.*", SearchOption.AllDirectories);
                    _fileIndex = 0;

                    ev = new ScanEvent { Status = "GUID scan: collecting files...", Progress01 = 0.55f };
                    return true;
                }

                if (_fileIndex >= _files.Length)
                {
                    ev = default;
                    return false;
                }

                string full = _files[_fileIndex++];
                ev = new ScanEvent
                {
                    Status = $"GUID scan: {_fileIndex}/{_files.Length}",
                    Progress01 = Mathf.Clamp01(0.55f + (_fileIndex / (float)Mathf.Max(1, _files.Length)) * 0.45f)
                };

                string ext = Path.GetExtension(full);
                if (string.IsNullOrEmpty(ext) || !_scanExts.Contains(ext.ToLowerInvariant()))
                    return true;

                // quick size guard
                FileInfo info;
                try { info = new FileInfo(full); }
                catch { return true; }

                if (info.Length <= 0 || info.Length > _maxFileBytes)
                    return true;

                string unityPath = FullPathToUnityPath(full);
                if (string.IsNullOrEmpty(unityPath)) return true;
                if (_onlyUnderAssets && !unityPath.StartsWith("Assets/", StringComparison.Ordinal)) return true;
                if (unityPath == _targetPath) return true;

                if (!PassCommonFilters(unityPath)) return true;

                // cache check
                long ticks = info.LastWriteTimeUtc.Ticks;
                if (_cache.TryGetValue(full, out var cached)
                    && cached.Length == info.Length
                    && cached.LastWriteTicksUtc == ticks)
                {
                    if (cached.Contains)
                        ev.FoundPath = unityPath;
                    return true;
                }

                bool contains = StreamContainsGuid(full, _targetGuid, _chunkBytes);

                _cache[full] = new FileCache
                {
                    Length = info.Length,
                    LastWriteTicksUtc = ticks,
                    Contains = contains
                };

                if (contains)
                    ev.FoundPath = unityPath;

                return true;
            }

            private bool PassCommonFilters(string unityPath)
            {
                // exclude folders
                for (int i = 0; i < _excludes.Count; i++)
                {
                    if (_excludes[i].IsExcluded(unityPath))
                        return false;
                }

                // name filter
                if (!string.IsNullOrEmpty(_nameFilterLower))
                {
                    string name = Path.GetFileNameWithoutExtension(unityPath).ToLowerInvariant();
                    if (name.IndexOf(_nameFilterLower, StringComparison.Ordinal) < 0)
                        return false;
                }

                return true;
            }

            private static bool StreamContainsGuid(string fullPath, string guid, int chunkBytes)
            {
                // Search for GUID string in a streaming way (no full read).
                // Safe for large YAML/text assets.
                try
                {
                    // GUID in Unity YAML is usually lowercase
                    string needle = guid;
                    int needleLen = needle.Length;

                    // keep overlap to not miss matches split across chunks
                    int overlap = Mathf.Clamp(needleLen - 1, 0, 63);

                    byte[] buffer = new byte[chunkBytes + overlap];
                    int bytesRead;
                    int carry = 0;

                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        while ((bytesRead = fs.Read(buffer, carry, chunkBytes)) > 0)
                        {
                            int total = carry + bytesRead;

                            // decode bytes to string (UTF8 is typical; fallback will just produce garbage but still might contain needle)
                            string text = Encoding.UTF8.GetString(buffer, 0, total);

                            if (text.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0)
                                return true;

                            // move last overlap bytes to front
                            if (overlap > 0)
                            {
                                carry = Mathf.Min(overlap, total);
                                Buffer.BlockCopy(buffer, total - carry, buffer, 0, carry);
                            }
                            else
                            {
                                carry = 0;
                            }
                        }
                    }
                }
                catch
                {
                    // probably binary or unreadable
                }

                return false;
            }

            private static string FullPathToUnityPath(string fullPath)
            {
                fullPath = fullPath.Replace('\\', '/');

                int idx = fullPath.LastIndexOf("/Assets/", StringComparison.OrdinalIgnoreCase);
                if (idx < 0)
                {
                    idx = fullPath.LastIndexOf("/Assets", StringComparison.OrdinalIgnoreCase);
                    if (idx < 0) return null;
                }

                return fullPath.Substring(idx + 1);
            }
        }

        private struct ScanEvent
        {
            public string Status;
            public float Progress01;
            public string FoundPath;
        }

        // ---------------- Exclude matchers ----------------
        private interface IExcludeMatcher
        {
            bool IsExcluded(string unityPath);
        }

        private sealed class PrefixExcludeMatcher : IExcludeMatcher
        {
            private readonly string _prefix; // e.g. "Assets/ThirdParty"
            public PrefixExcludeMatcher(string prefix)
            {
                _prefix = (prefix ?? "").Replace('\\', '/').Trim().TrimEnd('/');
            }

            public bool IsExcluded(string unityPath)
            {
                if (string.IsNullOrEmpty(_prefix)) return false;
                return unityPath.StartsWith(_prefix, StringComparison.Ordinal);
            }
        }

        private sealed class RegexExcludeMatcher : IExcludeMatcher
        {
            private readonly Regex _regex;
            public RegexExcludeMatcher(string pattern)
            {
                _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            public bool IsExcluded(string unityPath)
            {
                return _regex.IsMatch(unityPath);
            }
        }
    }
}
