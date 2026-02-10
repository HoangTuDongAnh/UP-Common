using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Collider helpers.
    /// </summary>
    public static class ColliderExtensions
    {
        /// <summary>
        /// Enable or disable collider safely.
        /// </summary>
        public static void SetEnabled(this Collider col, bool enabled)
        {
            if (col == null) return;
            col.enabled = enabled;
        }

        /// <summary>
        /// Check if a world point is inside collider bounds.
        /// </summary>
        public static bool ContainsPoint(this Collider col, Vector3 worldPoint)
        {
            if (col == null) return false;
            return col.bounds.Contains(worldPoint);
        }

        /// <summary>
        /// Get closest point on collider to a world position.
        /// </summary>
        public static Vector3 ClosestPointSafe(this Collider col, Vector3 worldPoint)
        {
            if (col == null) return worldPoint;
            return col.ClosestPoint(worldPoint);
        }
    }
}