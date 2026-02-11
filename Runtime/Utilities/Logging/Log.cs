using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Logging
{
    /// <summary>
    /// Simple logging wrapper.
    /// - Can be disabled by level
    /// - Optional compile define: UP_COMMON_LOG
    /// </summary>
    public static class Log
    {
        public static LogLevel Level { get; set; } =
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            LogLevel.Info;
#else
            LogLevel.Warning;
#endif

        [System.Diagnostics.Conditional("UP_COMMON_LOG")]
        public static void Info(object message)
        {
            if (Level < LogLevel.Info) return;
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional("UP_COMMON_LOG")]
        public static void Warn(object message)
        {
            if (Level < LogLevel.Warning) return;
            Debug.LogWarning(message);
        }

        public static void Error(object message)
        {
            if (Level < LogLevel.Error) return;
            Debug.LogError(message);
        }

        [System.Diagnostics.Conditional("UP_COMMON_LOG")]
        public static void Verbose(object message)
        {
            if (Level < LogLevel.Verbose) return;
            Debug.Log(message);
        }
    }
}