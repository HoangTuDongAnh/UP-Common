using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class CleanupEmptyGameObjectsTool
    {
        [MenuItem("Tools/UP-Common/Cleanup/Delete Empty GameObjects (Active Scene)")]
        private static void Cleanup()
        {
            bool ok = EditorUtility.DisplayDialog(
                "Delete Empty GameObjects",
                "This will delete GameObjects that have:\n- No components (except Transform)\n- No children\nin the ACTIVE scene.\n\nContinue?",
                "Delete",
                "Cancel"
            );

            if (!ok) return;

            var scene = SceneManager.GetActiveScene();
            if (!scene.IsValid())
            {
                Debug.LogWarning("Active scene is not valid.");
                return;
            }

            int removed = 0;

            var roots = scene.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++)
                removed += DeleteEmptyRecursive(roots[i].transform);

            Debug.Log($"Deleted {removed} empty GameObjects.");
        }

        private static int DeleteEmptyRecursive(Transform t)
        {
            int removed = 0;

            // traverse children first (reverse to be safe)
            for (int i = t.childCount - 1; i >= 0; i--)
                removed += DeleteEmptyRecursive(t.GetChild(i));

            // only Transform + no children => delete
            var comps = t.GetComponents<Component>();
            bool hasOnlyTransform = comps.Length == 1; // Transform always exists
            bool hasNoChildren = t.childCount == 0;

            if (hasOnlyTransform && hasNoChildren)
            {
                Object.DestroyImmediate(t.gameObject);
                return removed + 1;
            }

            return removed;
        }
    }
}