# FindReferencesUltimateWindow.cs

**Mô tả:** /// Find asset references via: /// 1) AssetDatabase dependency graph (fast) /// 2) GUID scan in YAML/text (stronger) - streaming + cache

**Đường dẫn:** `Editor/Tools/FindReferencesUltimateWindow.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Editor.Tools`

## Kiểu dữ liệu trong file
- `public class FindReferencesUltimateWindow`
- `private struct FileCache`
- `private struct ResultItem`
- `private class ScanState`
- `private struct ScanEvent`
- `private interface IExcludeMatcher`
- `private class PrefixExcludeMatcher`
- `private class RegexExcludeMatcher`

## Public/Protected API (tự trích)
### Methods
- `public bool Next(out ScanEvent ev)`
- `public bool IsExcluded(string unityPath)`

### Fields
- `public long Length`
- `public long LastWriteTicksUtc`
- `public bool Contains`
- `public string Path`
- `public string TypeName`
- `public Object Asset`
- `public string Status`
- `public float Progress01`
- `public string FoundPath`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
