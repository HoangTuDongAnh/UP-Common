using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// LineRenderer helpers.
    /// </summary>
    public static class LineRendererExtensions
    {
        /// <summary>
        /// Set positions from an array (null-safe).
        /// </summary>
        public static void SetPositionsSafe(this LineRenderer lr, Vector3[] positions)
        {
            if (lr == null || positions == null)
            {
                if (lr != null) lr.positionCount = 0;
                return;
            }

            lr.positionCount = positions.Length;
            lr.SetPositions(positions);
        }

        /// <summary>
        /// Set a simple line with 2 points.
        /// </summary>
        public static void SetLine(this LineRenderer lr, Vector3 a, Vector3 b)
        {
            if (lr == null) return;
            lr.positionCount = 2;
            lr.SetPosition(0, a);
            lr.SetPosition(1, b);
        }

        /// <summary>
        /// Clear line points.
        /// </summary>
        public static void Clear(this LineRenderer lr)
        {
            if (lr == null) return;
            lr.positionCount = 0;
        }
    }
}