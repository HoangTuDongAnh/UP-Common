using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class ClearPlayerPrefsTool
    {
        [MenuItem("Tools/UP-Common/Cleanup/Clear PlayerPrefs")]
        private static void Clear()
        {
            bool ok = EditorUtility.DisplayDialog(
                "Clear PlayerPrefs",
                "This will delete ALL PlayerPrefs keys for this project on this machine.\n\nDo you want to continue?",
                "Clear",
                "Cancel"
            );

            if (!ok) return;

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs cleared.");
        }
    }
}