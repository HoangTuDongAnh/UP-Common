using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Gizmos helpers for debug drawing.
    /// </summary>
    public static class GizmosExtensions
    {
        /// <summary>
        /// Draw a wire circle on XZ plane.
        /// </summary>
        public static void DrawWireCircleXZ(Vector3 center, float radius, int segments = 24)
        {
            if (radius <= 0f) return;
            if (segments < 3) segments = 3;

            float step = 360f / segments;
            Vector3 prev = center + new Vector3(radius, 0f, 0f);

            for (int i = 1; i <= segments; i++)
            {
                float angle = step * i * Mathf.Deg2Rad;
                Vector3 next = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
        }

        /// <summary>
        /// Draw a cross (3 axes) at position.
        /// </summary>
        public static void DrawCross(Vector3 center, float size = 0.25f)
        {
            if (size <= 0f) return;

            Gizmos.DrawLine(center + Vector3.right * size, center - Vector3.right * size);
            Gizmos.DrawLine(center + Vector3.up * size, center - Vector3.up * size);
            Gizmos.DrawLine(center + Vector3.forward * size, center - Vector3.forward * size);
        }

        /// <summary>
        /// Draw a bounding box for renderer (null-safe).
        /// </summary>
        public static void DrawRendererBounds(Renderer r)
        {
            if (r == null) return;
            Gizmos.DrawWireCube(r.bounds.center, r.bounds.size);
        }
    }
}