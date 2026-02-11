using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Safety
{
    /// <summary>
    /// UnityEngine.Object safe checks.
    /// </summary>
    public static class UnityObjectUtil
    {
        /// <summary>
        /// True if obj is not null and not destroyed.
        /// </summary>
        public static bool IsAlive(Object obj) => obj != null;

        /// <summary>
        /// Destroy safely.
        /// </summary>
        public static void DestroySafe(Object obj)
        {
            if (obj == null) return;
            Object.Destroy(obj);
        }
    }
}