# Runtime/Utilities/Safety

## Mục tiêu
Các helper “an toàn” để tránh bug:
- Guard (check null/condition)
- Runtime static reset entry (hỗ trợ Domain Reload Off)
- Disposable group (nếu có)

## Scripts
- `DisposableGroup.cs`
- `Guard.cs`
- `RuntimeStaticReset.cs`
- `UnityObjectUtil.cs`

## RuntimeStaticReset.cs
Entry non-generic để reset static singletons khi Unity load subsystem.  
Giúp tránh lỗi khi bật `Enter Play Mode Options` và tắt Domain Reload.

## Guard.cs (nếu có)
- Check điều kiện nhanh, throw rõ ràng.
- Dùng trong utility/manager để fail fast.
