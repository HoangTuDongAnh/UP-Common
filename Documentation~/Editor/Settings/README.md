# Editor/Settings

## Mục tiêu
Expose cấu hình UP-Common trong **Project Settings**.

## Scripts
- `UPCommonSettingsProvider.cs`

## UPCommonSettingsProvider
- Hiển thị mục `Project Settings > UP-Common`
- Cho phép chỉnh một số option (ví dụ log level)

> Nếu muốn persist setting giữa các máy: nên dùng ScriptableObject + serialized file.
