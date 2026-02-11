# EventChannel.cs

**Mô tả:** /// Typed channel for a single event type. /// Internal: used by EventBus.

**Đường dẫn:** `Runtime/Patterns/Observer/Internal/EventChannel.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.Observer.Internal`

## Kiểu dữ liệu trong file
- `internal class EventChannel<TEvent>`
- `private struct Slot`

## Public/Protected API (tự trích)
### Methods
- `public EventToken Subscribe(UnityEngine.Object owner, Action<TEvent> callback, Action<int, int> unsubscribeHook)`
- `public void Unsubscribe(int id, int version)`
- `public void UnregisterAll(UnityEngine.Object owner)`
- `public void Publish(in TEvent ev)`

### Fields
- `public int Id`
- `public int Version`
- `public UnityEngine.Object Owner`
- `public Action<TEvent> Callback`
- `public bool Active`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
