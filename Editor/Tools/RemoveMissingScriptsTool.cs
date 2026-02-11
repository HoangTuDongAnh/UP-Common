using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class RemoveMissingScriptsTool
    {
        [MenuItem("Tools/UP-Common/Cleanup/Remove Missing Scripts")]
        private static void Remove()
        {
#if UNITY_2022_2_OR_NEWER
            var objects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
#else
#pragma warning disable CS0618
            var objects = Object.FindObjectsOfType<GameObject>();
#pragma warning restore CS0618
#endif
            int count = 0;

            foreach (var go in objects)
            {
                int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                if (removed > 0) count += removed;
            }

            Debug.Log($"Removed {count} missing scripts.");
        }
    }
}