using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// GameObject helpers.
    /// </summary>
    public static class GameObjectExtensions
    {
        public static void SetLayerRecursively(this GameObject go, int layer)
        {
            if (go == null) return;

            go.layer = layer;
            var t = go.transform;

            for (int i = 0; i < t.childCount; i++)
                t.GetChild(i).gameObject.SetLayerRecursively(layer);
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) return null;

            var c = go.GetComponent<T>();
            return c != null ? c : go.AddComponent<T>();
        }
    }
}