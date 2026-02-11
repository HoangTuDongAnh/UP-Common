using UnityEditor;
using UnityEngine;
using HoangTuDongAnh.UP.Common.Utilities.Logging;

namespace HoangTuDongAnh.UP.Common.Editor.Settings
{
    public sealed class UPCommonSettingsProvider : SettingsProvider
    {
        private static LogLevel _logLevel = LogLevel.Info;

        public UPCommonSettingsProvider(string path, SettingsScope scope)
            : base(path, scope) { }

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            return new UPCommonSettingsProvider("Project/UP-Common", SettingsScope.Project);
        }

        public override void OnGUI(string searchContext)
        {
            GUILayout.Label("Logging", EditorStyles.boldLabel);
            _logLevel = (LogLevel)EditorGUILayout.EnumPopup("Log Level", _logLevel);
            Log.Level = _logLevel;
        }
    }
}