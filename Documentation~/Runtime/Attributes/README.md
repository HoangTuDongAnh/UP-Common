# Runtime/Attributes

Folder này chứa **các Attribute dùng trong Runtime** (được reference bởi gameplay scripts).  
Các **PropertyDrawer** tương ứng nằm ở `Editor/PropertyDrawers`.

## Danh sách script

- `MinMaxAttribute.cs`
- `ReadOnlyAttribute.cs`
- `ShowIfAttribute.cs`

## 1) ReadOnlyAttribute.cs
- Mục đích: hiển thị field ở Inspector nhưng **không cho chỉnh** (read-only).
- Dùng cho: debug info, state runtime, id sinh ra tự động.

**Ví dụ**
```csharp
using HoangTuDongAnh.UP.Common.Attributes;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    [ReadOnly] public int PlayerId;
}
```

## 2) ShowIfAttribute.cs
- Mục đích: **ẩn/hiện** field dựa trên 1 điều kiện bool (field/property) trong cùng object.
- Dùng cho: config có tuỳ chọn nâng cao, tránh Inspector rối.

**Ví dụ**
```csharp
public bool UseAdvanced;

[ShowIf(nameof(UseAdvanced))]
public float AdvancedValue;
```

## 3) MinMaxAttribute.cs
- Mục đích: vẽ `Vector2` dưới dạng **min-max slider**.
- Dùng cho: damage range, random range, spawn distance…

**Ví dụ**
```csharp
[MinMax(0, 100)]
public Vector2 DamageRange; // x=min, y=max
```

## Lưu ý
- Attribute phải nằm trong `Runtime/` để scripts runtime dùng được.
- Drawer nằm trong `Editor/` để không ảnh hưởng build.
