# Editor/Tools

## Mục tiêu
Các công cụ menu dưới `Tools/UP-Common/...` giúp dọn dẹp và thao tác nhanh trong Editor.

## Scripts
- `BatchRenameAssetsWindow.cs`
- `CleanUpEmptyGameObjectsTool.cs`
- `ClearPlayerPrefsTool.cs`
- `FindReferencesUltimateWindow.cs`
- `HierarchyExpandCollapseTool.cs`
- `OpenCommonPathsTool.cs`
- `PlayModeOptionsTool.cs`
- `RemoveMissingScriptsTool.cs`
- `SelectMissingScriptsTool.cs`

## 1) RemoveMissingScriptsTool
- Xoá Missing Script trên tất cả GameObject.
- Unity mới dùng `FindObjectsByType(...None)` (nhanh, không warning).

## 2) SelectMissingScriptsTool
- Tự động select các GameObject đang bị Missing Script.

## 3) CleanUpEmptyGameObjectsTool
- Xoá GameObject rỗng (chỉ Transform, không child) trong active scene.

## 4) ClearPlayerPrefsTool
- Xoá toàn bộ PlayerPrefs (có confirm).

## 5) OpenCommonPathsTool
- Mở nhanh các path: `Assets/`, `persistentDataPath`, log path (tuỳ OS).

## 6) PlayModeOptionsTool
- Toggle Enter Play Mode Options / Domain Reload / Scene Reload.

## 7) HierarchyExpandCollapseTool
- Expand/Collapse selected trong Hierarchy (dùng reflection để tương thích nhiều Unity version).

## 8) FindReferencesUltimateWindow
- Tìm asset reference trong project (dependency graph + GUID scan).
- Có lọc folder, export, ping/open (tuỳ phiên bản hiện tại trong repo).

## 9) BatchRenameAssetsWindow
- Đổi tên hàng loạt asset theo prefix/suffix/replace/index.

### Best practices
- MenuItem path không được trùng (Unity sẽ báo conflict).
- Nên gộp tool “Open Path” để tránh duplicate menu.
