# EventBus.cs

**Mô tả:** /// Typed event bus. /// - Token-based unsubscribe /// - Owner-based auto cleanup /// - Optional cross-thread publish (queued to main thread)

**Đường dẫn:** `Runtime/Patterns/Observer/EventBus.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.Observer`

## Kiểu dữ liệu trong file
- `public class EventBus`
- `private interface IChannel`
- `private class ChannelWrapper<TEvent>`

## Public/Protected API (tự trích)
### Methods
- `public void UnregisterAll(UnityEngine.Object owner)`
- `public void ClearAll()`

### Fields
- `public EventChannel<TEvent> Channel`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
