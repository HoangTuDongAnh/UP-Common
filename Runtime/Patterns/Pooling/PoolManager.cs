using System.Collections.Generic;
using HoangTuDongAnh.UP.Common.Patterns.Pooling.Internal;
using HoangTuDongAnh.UP.Common.Patterns.Singleton;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Pooling
{
    /// <summary>
    /// GameObject pooling manager.
    /// - Fast spawn/despawn
    /// - Auto tracks prefab per instance (no prefab param on Despawn)
    /// - Optional collection checks for safety
    /// - Supports warm up and max size
    /// </summary>
    public sealed class PoolManager : MonoSingleton<PoolManager>
    {
        private sealed class PoolData
        {
            public GameObject Prefab;
            public readonly Queue<GameObject> Inactive = new Queue<GameObject>(32);
            public Transform Parent;
            public int MaxSize; // <= 0 means unlimited

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            public readonly HashSet<int> AllInstanceIds = new HashSet<int>(); // for validation
#endif
        }

        // Key by prefab instance id
        private readonly Dictionary<int, PoolData> _pools = new Dictionary<int, PoolData>(32);

        /// <summary>
        /// Enable safety checks (Editor/Dev only recommended).
        /// </summary>
        public bool EnableCollectionCheck { get; set; } =
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            true;
#else
            false;
#endif

        /// <summary>
        /// Default max size for pools created without explicit maxSize.
        /// Set <= 0 for unlimited.
        /// </summary>
        public int DefaultMaxSize { get; set; } = 0;

        // -------------------------
        // Public API
        // -------------------------

        /// <summary>
        /// Pre-create instances and keep them inactive in pool.
        /// </summary>
        public void WarmUp(GameObject prefab, int amount, int maxSize = 0)
        {
            if (prefab == null || amount <= 0) return;

            var pool = GetOrCreatePool(prefab, maxSize);
            for (int i = 0; i < amount; i++)
            {
                var obj = CreateNew(prefab, pool);
                ReturnInternal(obj, pool);
            }
        }

        /// <summary>
        /// Spawn a pooled GameObject.
        /// </summary>
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null, int maxSize = 0)
        {
            if (prefab == null) return null;

            var pool = GetOrCreatePool(prefab, maxSize);
            var obj = (pool.Inactive.Count > 0) ? pool.Inactive.Dequeue() : CreateNew(prefab, pool);

            var item = obj.GetComponent<PoolItem>(); // always exists
            item.InPool = false;

            var t = obj.transform;
            t.SetParent(parent, worldPositionStays: false);
            t.SetPositionAndRotation(position, rotation);

            obj.SetActive(true);
            item.Poolable?.OnSpawn();

            return obj;
        }

        /// <summary>
        /// Spawn and return a component on the spawned object.
        /// </summary>
        public T Spawn<T>(T prefabComponent, Vector3 position, Quaternion rotation, Transform parent = null, int maxSize = 0)
            where T : Component
        {
            if (prefabComponent == null) return null;

            var go = Spawn(prefabComponent.gameObject, position, rotation, parent, maxSize);
            return go != null ? go.GetComponent<T>() : null;
        }

        /// <summary>
        /// Return object to pool (safe).
        /// </summary>
        public void Despawn(GameObject obj)
        {
            TryDespawn(obj);
        }

        /// <summary>
        /// Return component's GameObject to pool.
        /// </summary>
        public void Despawn(Component component)
        {
            if (component == null) return;
            TryDespawn(component.gameObject);
        }

        /// <summary>
        /// Try despawn. Returns false if object is not pooled/invalid.
        /// </summary>
        public bool TryDespawn(GameObject obj)
        {
            if (obj == null) return false;

            var item = obj.GetComponent<PoolItem>();
            if (item == null)
            {
                // Not created by PoolManager.
                obj.SetActive(false);
                return false;
            }

            if (item.InPool) return true; // already in pool

            if (!_pools.TryGetValue(item.PrefabKey, out var pool) || pool == null)
            {
                Destroy(obj);
                return false;
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (EnableCollectionCheck && !pool.AllInstanceIds.Contains(obj.GetInstanceID()))
            {
                Debug.LogWarning($"[PoolManager] Despawn failed: object not owned by this pool. Obj={obj.name}", obj);
                Destroy(obj);
                return false;
            }
#endif

            item.Poolable?.OnDespawn();
            obj.SetActive(false);
            ReturnInternal(obj, pool);
            return true;
        }

        /// <summary>
        /// Clear one prefab pool (destroys inactive objects).
        /// </summary>
        public void Clear(GameObject prefab)
        {
            if (prefab == null) return;

            int key = prefab.GetInstanceID();
            if (!_pools.TryGetValue(key, out var pool)) return;

            DestroyPool(pool);
            _pools.Remove(key);
        }

        /// <summary>
        /// Clear all pools.
        /// </summary>
        public void ClearAll()
        {
            foreach (var kv in _pools)
                DestroyPool(kv.Value);

            _pools.Clear();
        }

        // -------------------------
        // Internal
        // -------------------------

        private PoolData GetOrCreatePool(GameObject prefab, int maxSize)
        {
            int key = prefab.GetInstanceID();

            if (_pools.TryGetValue(key, out var pool))
            {
                // Allow setting max size once.
                if (pool.MaxSize <= 0 && maxSize > 0) pool.MaxSize = maxSize;
                return pool;
            }

            var parentObj = new GameObject($"Pool_{prefab.name}");
            parentObj.transform.SetParent(transform, worldPositionStays: false);

            pool = new PoolData
            {
                Prefab = prefab,
                Parent = parentObj.transform,
                MaxSize = (maxSize > 0) ? maxSize : DefaultMaxSize
            };

            _pools.Add(key, pool);
            return pool;
        }

        private GameObject CreateNew(GameObject prefab, PoolData pool)
        {
            var go = Instantiate(prefab);
            go.name = prefab.name;

            var item = go.GetComponent<PoolItem>();
            if (item == null) item = go.AddComponent<PoolItem>();

            item.PrefabKey = prefab.GetInstanceID();
            item.InPool = false;
            item.Poolable = go.GetComponent<IPoolable>(); // cache once

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            pool.AllInstanceIds.Add(go.GetInstanceID());
#endif
            // Start inactive when created for pool usage.
            go.SetActive(false);
            return go;
        }

        private void ReturnInternal(GameObject obj, PoolData pool)
        {
            var item = obj.GetComponent<PoolItem>();
            item.InPool = true;

            // Keep hierarchy clean.
            obj.transform.SetParent(pool.Parent, worldPositionStays: false);

            // Max size: destroy overflow.
            if (pool.MaxSize > 0 && pool.Inactive.Count >= pool.MaxSize)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (EnableCollectionCheck) pool.AllInstanceIds.Remove(obj.GetInstanceID());
#endif
                Destroy(obj);
                return;
            }

            pool.Inactive.Enqueue(obj);
        }

        private void DestroyPool(PoolData pool)
        {
            while (pool.Inactive.Count > 0)
            {
                var go = pool.Inactive.Dequeue();
                if (go != null) Destroy(go);
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            pool.AllInstanceIds.Clear();
#endif
            if (pool.Parent != null) Destroy(pool.Parent.gameObject);
        }
    }
}
