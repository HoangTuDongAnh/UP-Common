# Editor

`Editor/` chứa code chỉ chạy trong Unity Editor (không vào build).

## Nhóm
- PropertyDrawers: vẽ Inspector cho Runtime Attributes
- Tools: menu tools hỗ trợ workflow
- Generators: tool generate code
- Settings: SettingsProvider / cấu hình project

## Lưu ý quan trọng
- File Editor bắt buộc nằm trong folder `Editor/` (hoặc asmdef Editor-only)
- Tránh trùng `MenuItem` path (Unity sẽ báo conflict)
