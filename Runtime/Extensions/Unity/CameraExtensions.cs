using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Camera helpers.
    /// </summary>
    public static class CameraExtensions
    {
        /// <summary>
        /// World point -> screen point (null-safe).
        /// </summary>
        public static Vector3 WorldToScreenPointSafe(this Camera cam, Vector3 worldPos)
        {
            return cam != null ? cam.WorldToScreenPoint(worldPos) : Vector3.zero;
        }

        /// <summary>
        /// Screen point -> world point at a given z distance from camera.
        /// </summary>
        public static Vector3 ScreenToWorldPointAtDistance(this Camera cam, Vector3 screenPos, float distance)
        {
            if (cam == null) return Vector3.zero;
            screenPos.z = distance;
            return cam.ScreenToWorldPoint(screenPos);
        }

        /// <summary>
        /// Check if a world point is inside camera viewport.
        /// Optionally require point to be in front of camera.
        /// </summary>
        public static bool IsWorldPointVisible(this Camera cam, Vector3 worldPos, bool requireInFront = true)
        {
            if (cam == null) return false;

            var vp = cam.WorldToViewportPoint(worldPos);

            if (requireInFront && vp.z <= 0f) return false;

            return vp.x >= 0f && vp.x <= 1f &&
                   vp.y >= 0f && vp.y <= 1f;
        }

        /// <summary>
        /// Get a ray from camera through screen position (null-safe).
        /// </summary>
        public static Ray ScreenPointToRaySafe(this Camera cam, Vector3 screenPos)
        {
            return cam != null ? cam.ScreenPointToRay(screenPos) : new Ray(Vector3.zero, Vector3.forward);
        }
    }
}