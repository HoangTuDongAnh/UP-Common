namespace HoangTuDongAnh.UP.Common.Patterns.Pooling
{
    /// <summary>
    /// Optional callbacks for pooled objects.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Called when the object is taken from pool.
        /// </summary>
        void OnSpawn();

        /// <summary>
        /// Called before the object is returned to pool.
        /// </summary>
        void OnDespawn();
    }
}