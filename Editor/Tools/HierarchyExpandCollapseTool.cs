using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class HierarchyExpandCollapseTool
    {
        [MenuItem("Tools/UP-Common/Hierarchy/Expand Selected %#e")] // Ctrl/Cmd+Shift+E
        private static void Expand()
        {
            foreach (var obj in Selection.gameObjects)
                SetExpandedRecursive(obj, true);
        }

        [MenuItem("Tools/UP-Common/Hierarchy/Collapse Selected %#c")] // Ctrl/Cmd+Shift+C
        private static void Collapse()
        {
            foreach (var obj in Selection.gameObjects)
                SetExpandedRecursive(obj, false);
        }

        private static void SetExpandedRecursive(GameObject go, bool expanded)
        {
            if (go == null) return;

            // Works across Unity versions by calling internal hierarchy method.
            TrySetHierarchyExpanded(go.GetInstanceID(), expanded);
        }

        private static void TrySetHierarchyExpanded(int instanceId, bool expanded)
        {
            var sceneHierarchyType = Type.GetType("UnityEditor.SceneHierarchyWindow, UnityEditor");
            if (sceneHierarchyType == null) return;

            var window = EditorWindow.GetWindow(sceneHierarchyType);
            if (window == null) return;

            // Method signature varies by Unity version -> try common ones.
            var method = sceneHierarchyType.GetMethod("SetExpandedRecursive",
                             BindingFlags.Instance | BindingFlags.NonPublic,
                             null,
                             new[] { typeof(int), typeof(bool) },
                             null);

            if (method != null)
            {
                method.Invoke(window, new object[] { instanceId, expanded });
                return;
            }

            // Fallback: older signature (int) only
            method = sceneHierarchyType.GetMethod("SetExpandedRecursive",
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        new[] { typeof(int) },
                        null);

            if (method != null)
            {
                if (expanded)
                    method.Invoke(window, new object[] { instanceId });
                else
                    EditorApplication.RepaintHierarchyWindow(); // best-effort collapse fallback
            }
        }
    }
}
