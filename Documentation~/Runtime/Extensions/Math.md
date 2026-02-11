# Runtime/Extensions/Math

## Mục tiêu
Helper toán học, đặc biệt cho Vector2/Vector3/Quaternion, clamp, lerp, direction...

## Scripts
- `BoundsExtensions.cs`
- `ColorExtensions.cs`
- `FloatExtensions.cs`
- `IntExtensions.cs`
- `QuaternionExtensions.cs`
- `RandomExtensions.cs`
- `Vector2Extensions.cs`
- `Vector3Extensions.cs`

## Ví dụ (Vector3Extensions)
```csharp
using HoangTuDongAnh.UP.Common.Extensions.Math;

var v = transform.position;
v = v.WithY(0f).AddX(2f);
```

## Best practices
- Extensions nên thuần (không allocate)
- Với phép tính lặp nhiều: ưu tiên `sqrMagnitude` khi có thể
