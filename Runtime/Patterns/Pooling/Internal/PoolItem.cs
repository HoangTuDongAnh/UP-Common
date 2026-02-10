using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Pooling.Internal
{
    /// <summary>
    /// Internal metadata for pooled instances.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PoolItem : MonoBehaviour
    {
        public int PrefabKey;          // prefab instance id
        public bool InPool;            // prevents double-despawn
        public IPoolable Poolable;     // cached callback
    }
}