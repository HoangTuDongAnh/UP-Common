using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class OpenCommonPathsTool
    {
        [MenuItem("Tools/UP-Common/Open/Data Path")]
        private static void OpenDataPath() => EditorUtility.RevealInFinder(Application.dataPath);

        [MenuItem("Tools/UP-Common/Open/Persistent Data Path")]
        private static void OpenPersistent() => EditorUtility.RevealInFinder(Application.persistentDataPath);

        [MenuItem("Tools/UP-Common/Open/Console Log Path")]
        private static void OpenConsoleLogPath()
        {
#if UNITY_EDITOR_WIN
            // Unity Editor log path (Windows)
            EditorUtility.RevealInFinder(System.Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\Unity\\Editor\\Editor.log"));
#elif UNITY_EDITOR_OSX
            EditorUtility.RevealInFinder(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/Library/Logs/Unity/Editor.log");
#else
            Debug.LogWarning("Console log path is not configured for this platform.");
#endif
        }
    }
}