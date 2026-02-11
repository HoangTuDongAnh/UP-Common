# Hướng dẫn sử dụng: Pooling

Pooling giúp giảm instantiate/destroy, tránh spike.

## 1) Implement IPoolable
```csharp
public class Bullet : MonoBehaviour, IPoolable
{
    public void OnSpawn() => gameObject.SetActive(true);
    public void OnDespawn() => gameObject.SetActive(false);
}
```

## 2) Spawn/Despawn
```csharp
var bullet = PoolManager.Instance.Spawn(bulletPrefab, pos, rot);
PoolManager.Instance.Despawn(bullet);
```

## Lưu ý
- Nên reset state khi despawn (velocity, trail, animation…)
- Prewarm pool cho object spawn nhiều (nếu PoolManager có hỗ trợ)
