namespace HoangTuDongAnh.UP.Common.Utilities.Time
{
    /// <summary>
    /// Simple timer. Call Tick(deltaTime).
    /// </summary>
    public struct Timer
    {
        public float Duration { get; private set; }
        public float Elapsed { get; private set; }
        public bool IsRunning { get; private set; }

        public float Remaining => Duration - Elapsed;
        public float Normalized => Duration <= 0f ? 1f : Elapsed / Duration;

        public Timer(float duration)
        {
            Duration = duration;
            Elapsed = 0f;
            IsRunning = false;
        }

        public void Start(float duration)
        {
            Duration = duration;
            Elapsed = 0f;
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;

        public bool Tick(float deltaTime)
        {
            if (!IsRunning) return false;

            Elapsed += deltaTime;
            if (Elapsed >= Duration)
            {
                Elapsed = Duration;
                IsRunning = false;
                return true; // finished
            }

            return false;
        }
    }
}