# Runtime/Patterns/Pooling

## Mục tiêu
Object pooling để:
- giảm instantiate/destroy liên tục (giảm GC & spike)
- tái sử dụng object nhanh
- quản lý lifecycle rõ ràng

## Scripts
- `IPoolable.cs`
- `PoolManager.cs`
- `Internal/*` (nếu có: PoolItem, cấu trúc pool)

---

## 1) IPoolable.cs
Contract cho object có thể được pool:
- OnSpawn / OnDespawn (hoặc tương đương)

**Ví dụ**
```csharp
public class Bullet : MonoBehaviour, IPoolable
{
    public void OnSpawn() { gameObject.SetActive(true); }
    public void OnDespawn() { gameObject.SetActive(false); }
}
```

## 2) PoolManager.cs
- tạo pool theo prefab/key
- spawn / despawn
- prewarm (nếu có)

**Ví dụ**
```csharp
var bullet = PoolManager.Instance.Spawn(bulletPrefab, pos, rot);
PoolManager.Instance.Despawn(bullet);
```

## Best practices
- Pool size nên prewarm cho object spawn nhiều
- Đừng pool object “rất hiếm dùng” (không cần)
- Khi despawn: reset state (velocity, animation, trail…)
