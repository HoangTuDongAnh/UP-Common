using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.Tools
{
    public static class PlayModeOptionsTool
    {
        [MenuItem("Tools/UP-Common/Play Mode/Toggle Enter Play Mode Options")]
        private static void ToggleEnterPlayModeOptions()
        {
            EditorSettings.enterPlayModeOptionsEnabled = !EditorSettings.enterPlayModeOptionsEnabled;

            Debug.Log($"Enter Play Mode Options Enabled: {EditorSettings.enterPlayModeOptionsEnabled}");
        }

        [MenuItem("Tools/UP-Common/Play Mode/Toggle Domain Reload")]
        private static void ToggleDomainReload()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;

            var opts = EditorSettings.enterPlayModeOptions;
            bool domainReloadDisabled = (opts & EnterPlayModeOptions.DisableDomainReload) != 0;

            if (domainReloadDisabled)
                opts &= ~EnterPlayModeOptions.DisableDomainReload;
            else
                opts |= EnterPlayModeOptions.DisableDomainReload;

            EditorSettings.enterPlayModeOptions = opts;

            Debug.Log($"Disable Domain Reload: {(EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableDomainReload) != 0}");
        }

        [MenuItem("Tools/UP-Common/Play Mode/Toggle Scene Reload")]
        private static void ToggleSceneReload()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;

            var opts = EditorSettings.enterPlayModeOptions;
            bool sceneReloadDisabled = (opts & EnterPlayModeOptions.DisableSceneReload) != 0;

            if (sceneReloadDisabled)
                opts &= ~EnterPlayModeOptions.DisableSceneReload;
            else
                opts |= EnterPlayModeOptions.DisableSceneReload;

            EditorSettings.enterPlayModeOptions = opts;

            Debug.Log($"Disable Scene Reload: {(EditorSettings.enterPlayModeOptions & EnterPlayModeOptions.DisableSceneReload) != 0}");
        }
    }
}