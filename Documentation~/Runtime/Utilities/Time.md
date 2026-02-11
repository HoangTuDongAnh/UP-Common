# Runtime/Utilities/Time

## Mục tiêu
Cung cấp các utility về thời gian:
- Timer / Countdown
- Stopwatch nhẹ
- Stack timeScale (nếu có)

## Scripts
- `CountdownTimer.cs`
- `FrameTimer.cs`
- `StopwatchLite.cs`
- `TimeScaleStack.cs`
- `Timer.cs`

## Ví dụ (Timer)
```csharp
var timer = new Timer(1.5f);
timer.Tick(UnityEngine.Time.deltaTime);
if (timer.IsDone) { /* ... */ }
```

## Best practices
- Tách time logic khỏi MonoBehaviour khi có thể
- Chọn DeltaTime phù hợp (scaled/unscaled) theo nhu cầu
