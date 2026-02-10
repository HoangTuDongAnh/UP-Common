using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Component helpers.
    /// </summary>
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this Component c) where T : Component
        {
            if (c == null) return null;
            return c.gameObject.GetOrAddComponent<T>();
        }

        public static bool TryGetComponentInParent<T>(this Component c, out T result) where T : Component
        {
            result = null;
            if (c == null) return false;

            result = c.GetComponentInParent<T>();
            return result != null;
        }

        public static bool TryGetComponentInChildren<T>(this Component c, out T result) where T : Component
        {
            result = null;
            if (c == null) return false;

            result = c.GetComponentInChildren<T>();
            return result != null;
        }
    }
}