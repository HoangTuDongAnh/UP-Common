using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Time
{
    /// <summary>
    /// Simple realtime stopwatch (no allocations).
    /// </summary>
    public struct StopwatchLite
    {
        private float _start;
        public bool IsRunning { get; private set; }

        public void Start()
        {
            _start = UnityEngine.Time.realtimeSinceStartup;
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;

        public float ElapsedSeconds
        {
            get
            {
                if (!IsRunning) return 0f;
                return UnityEngine.Time.realtimeSinceStartup - _start;
            }
        }

        public float ElapsedMilliseconds => ElapsedSeconds * 1000f;
    }
}