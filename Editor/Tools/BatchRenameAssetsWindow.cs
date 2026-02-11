using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public sealed class BatchRenameAssetsWindow : EditorWindow
    {
        private string _prefix = "";
        private string _suffix = "";
        private string _replaceFrom = "";
        private string _replaceTo = "";
        private bool _useIndex = false;
        private int _startIndex = 0;
        private int _pad = 2;

        [MenuItem("Tools/UP-Common/Assets/Batch Rename Assets")]
        private static void Open()
        {
            GetWindow<BatchRenameAssetsWindow>("Batch Rename");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Batch Rename (Selected Assets)", EditorStyles.boldLabel);

            _prefix = EditorGUILayout.TextField("Prefix", _prefix);
            _suffix = EditorGUILayout.TextField("Suffix", _suffix);

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("Replace", EditorStyles.boldLabel);
            _replaceFrom = EditorGUILayout.TextField("From", _replaceFrom);
            _replaceTo = EditorGUILayout.TextField("To", _replaceTo);

            EditorGUILayout.Space(4);
            _useIndex = EditorGUILayout.Toggle("Append Index", _useIndex);
            using (new EditorGUI.DisabledScope(!_useIndex))
            {
                _startIndex = EditorGUILayout.IntField("Start Index", _startIndex);
                _pad = EditorGUILayout.IntSlider("Zero Pad", _pad, 0, 6);
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Rename Selected"))
            {
                RenameSelected();
            }

            EditorGUILayout.HelpBox(
                "Tip: Select assets in Project window (not scene objects).",
                MessageType.Info
            );
        }

        private void RenameSelected()
        {
            var objs = Selection.objects;
            if (objs == null || objs.Length == 0)
            {
                Debug.LogWarning("No assets selected.");
                return;
            }

            // Only rename real assets (skip folders).
            var paths = new List<string>(objs.Length);
            for (int i = 0; i < objs.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(objs[i]);
                if (string.IsNullOrEmpty(path)) continue;
                if (AssetDatabase.IsValidFolder(path)) continue;
                paths.Add(path);
            }

            if (paths.Count == 0)
            {
                Debug.LogWarning("No valid assets selected (folders are ignored).");
                return;
            }

            bool ok = EditorUtility.DisplayDialog(
                "Batch Rename",
                $"Rename {paths.Count} assets?\nThis cannot be undone easily.",
                "Rename",
                "Cancel"
            );

            if (!ok) return;

            AssetDatabase.StartAssetEditing();
            try
            {
                int index = _startIndex;

                for (int i = 0; i < paths.Count; i++)
                {
                    string path = paths[i];
                    string oldName = System.IO.Path.GetFileNameWithoutExtension(path);

                    string name = oldName;

                    if (!string.IsNullOrEmpty(_replaceFrom))
                        name = name.Replace(_replaceFrom, _replaceTo ?? "");

                    name = $"{_prefix}{name}{_suffix}";

                    if (_useIndex)
                        name = $"{name}_{index.ToString().PadLeft(_pad, '0')}";

                    string err = AssetDatabase.RenameAsset(path, name);
                    if (!string.IsNullOrEmpty(err))
                        Debug.LogWarning($"Rename failed: {path} => {name} ({err})");

                    index++;
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }

            Debug.Log("Batch rename completed.");
        }
    }
}
