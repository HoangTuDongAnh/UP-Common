# Editor/PropertyDrawers

## Mục tiêu
Vẽ UI đẹp cho các Attribute trong `Runtime/Attributes`.

## Scripts
- `MinMaxDrawer.cs`
- `ReadOnlyDrawer.cs`
- `ShowIfDrawer.cs`

## ReadOnlyDrawer
- Khóa GUI để field không sửa được.

## ShowIfDrawer
- Ẩn/hiện field dựa theo bool condition member.

## MinMaxDrawer
- Vẽ Vector2 dạng min-max slider.

> Nếu bạn thấy “Cannot resolve symbol PropertyDrawer/CustomPropertyDrawer”:
- đảm bảo file nằm trong `Editor/`
- có `using UnityEditor;`
- Editor asmdef đúng includePlatforms Editor
