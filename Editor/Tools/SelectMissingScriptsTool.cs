using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class SelectMissingScriptsTool
    {
        [MenuItem("Tools/UP-Common/Cleanup/Select Objects With Missing Scripts")]
        private static void Select()
        {
#if UNITY_2022_2_OR_NEWER
            var all = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
#else
#pragma warning disable CS0618
            var all = Object.FindObjectsOfType<GameObject>();
#pragma warning restore CS0618
#endif
            var list = new List<GameObject>(256);

            foreach (var go in all)
            {
                if (go == null) continue;

                var components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        list.Add(go);
                        break;
                    }
                }
            }

            Selection.objects = list.ToArray();
            Debug.Log($"Selected {list.Count} objects with missing scripts.");
        }
    }
}