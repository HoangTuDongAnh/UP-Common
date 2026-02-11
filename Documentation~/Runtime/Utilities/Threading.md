# Runtime/Utilities/Threading

## Mục tiêu
Đưa callback về main thread để gọi Unity API an toàn.

## Scripts
- `MainThreadDispatcher.cs`
- `ThreadSafeQueue.cs`
- `UnitySyncContext.cs`

## Cách dùng
```csharp
using HoangTuDongAnh.UP.Common.Utilities.Threading;

MainThreadDispatcher.Enqueue(() =>
{
    // Unity API here
});
```

## Khi nào cần?
- Kết quả từ Task/Thread/async cần cập nhật UI, spawn object…
