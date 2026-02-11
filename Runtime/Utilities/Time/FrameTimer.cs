namespace HoangTuDongAnh.UP.Common.Utilities.Time
{
    /// <summary>
    /// Frame-based timer. Useful for cooldown by frames.
    /// </summary>
    public struct FrameTimer
    {
        public int DurationFrames { get; private set; }
        public int ElapsedFrames { get; private set; }
        public bool IsRunning { get; private set; }

        public FrameTimer(int durationFrames)
        {
            DurationFrames = durationFrames;
            ElapsedFrames = 0;
            IsRunning = false;
        }

        public void Start(int durationFrames)
        {
            DurationFrames = durationFrames;
            ElapsedFrames = 0;
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;

        public bool Tick()
        {
            if (!IsRunning) return false;

            ElapsedFrames++;
            if (ElapsedFrames >= DurationFrames)
            {
                IsRunning = false;
                return true;
            }

            return false;
        }
    }
}