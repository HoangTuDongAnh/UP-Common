using System;

namespace HoangTuDongAnh.UP.Common.Utilities.Time
{
    /// <summary>
    /// Countdown timer with optional callback.
    /// </summary>
    public sealed class CountdownTimer
    {
        public float Duration { get; private set; }
        public float Remaining { get; private set; }
        public bool IsRunning { get; private set; }

        private readonly Action _onFinished;

        public CountdownTimer(float duration, Action onFinished = null)
        {
            Duration = duration;
            Remaining = duration;
            _onFinished = onFinished;
        }

        public void Start(float duration)
        {
            Duration = duration;
            Remaining = duration;
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;

        public void Reset()
        {
            Remaining = Duration;
            IsRunning = false;
        }

        public void Tick(float deltaTime)
        {
            if (!IsRunning) return;

            Remaining -= deltaTime;
            if (Remaining <= 0f)
            {
                Remaining = 0f;
                IsRunning = false;
                _onFinished?.Invoke();
            }
        }
    }
}