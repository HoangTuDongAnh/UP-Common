using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Collider2D helpers.
    /// </summary>
    public static class Collider2DExtensions
    {
        public static void SetEnabled(this Collider2D col, bool enabled)
        {
            if (col == null) return;
            col.enabled = enabled;
        }

        public static Vector2 ClosestPointSafe(this Collider2D col, Vector2 worldPoint)
        {
            if (col == null) return worldPoint;
            return col.ClosestPoint(worldPoint);
        }
    }
}