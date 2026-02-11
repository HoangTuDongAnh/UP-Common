# Runtime/Extensions/Unity

## Mục tiêu
Helper cho Unity API để viết gọn, an toàn hơn:
- Transform/GameObject/Component helpers
- Coroutine helpers (nếu có)
- Destroy-safe checks

## Scripts
- `AnimatorExtensions.cs`
- `AnimatorHashExtensions.cs`
- `AudioSourceExtensions.cs`
- `CameraExtensions.cs`
- `CanvasExtensions.cs`
- `Collider2DExtensions.cs`
- `ColliderExtensions.cs`
- `ComponentExtensions.cs`
- `GameObjectExtensions.cs`
- `GizmosExtensions.cs`
- `LayerMaskExtensions.cs`
- `LineRendererExtensions.cs`
- `MonoBehaviourExtensions.cs`
- `NavMeshExtensions.cs`
- `ParticleSystemExtensions.cs`
- `PhysicsExtensions.cs`
- `RectTransformExtensions.cs`
- `RendererExtensions.cs`
- `Rigidbody2DExtensions.cs`
- `RigidbodyExtensions.cs`
- `TransformExtensions.cs`

## Ví dụ
```csharp
using HoangTuDongAnh.UP.Common.Extensions.Unity;

// ví dụ: GetOrAddComponent
var rb = gameObject.GetOrAddComponent<Rigidbody>();
```

## Lưu ý
- Extensions Unity nên tránh tạo GC
- Với API thay đổi theo Unity version: dùng `#if UNITY_...` như bạn đã làm ở Editor tools
