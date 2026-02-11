# PoolManager.cs

**Mô tả:** /// GameObject pooling manager. /// - Fast spawn/despawn /// - Auto tracks prefab per instance (no prefab param on Despawn) /// - Optional collection checks for safety /// - Supports warm up and max size

**Đường dẫn:** `Runtime/Patterns/Pooling/PoolManager.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.Pooling`

## Kiểu dữ liệu trong file
- `public class PoolManager`
- `private class PoolData`

## Public/Protected API (tự trích)
### Methods
- `public void WarmUp(GameObject prefab, int amount, int maxSize = 0)`
- `public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null, int maxSize = 0)`
- `public void Despawn(GameObject obj)`
- `public bool TryDespawn(GameObject obj)`
- `public void Clear(GameObject prefab)`
- `public void ClearAll()`

### Properties
- `public bool EnableCollectionCheck`
- `public int DefaultMaxSize`

### Fields
- `public GameObject Prefab`
- `public Queue<GameObject> Inactive`
- `public Transform Parent`
- `public int MaxSize`
- `public HashSet<int> AllInstanceIds`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
