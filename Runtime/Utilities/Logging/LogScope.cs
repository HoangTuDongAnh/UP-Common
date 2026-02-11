using System;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Logging
{
    /// <summary>
    /// Measure code block time with "using".
    /// </summary>
    public readonly struct LogScope : IDisposable
    {
        private readonly string _name;
        private readonly float _startTime;
        private readonly LogLevel _level;

        public LogScope(string name, LogLevel level = LogLevel.Info)
        {
            _name = name;
            _level = level;
            _startTime = UnityEngine.Time.realtimeSinceStartup;

#if UP_COMMON_LOG
            if (Log.Level >= _level)
                Debug.Log($"[Scope] {_name} start");
#endif
        }

        public void Dispose()
        {
#if UP_COMMON_LOG
            if (Log.Level < _level) return;

            float ms =
                (UnityEngine.Time.realtimeSinceStartup - _startTime) * 1000f; // ✅ FIX

            Debug.Log($"[Scope] {_name} end ({ms:0.00} ms)");
#endif
        }
    }
}