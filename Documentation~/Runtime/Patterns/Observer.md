# Runtime/Patterns/Observer

## Mục tiêu
Cung cấp **Event Bus typed** (Observer Pattern) với các ưu điểm:
- Event là struct typed (ít GC, rõ ràng)
- Subscribe trả về `EventToken` để unsubscribe an toàn
- Hỗ trợ owner-based auto cleanup
- Tuỳ chọn publish từ thread khác (queue về main thread)

## Scripts
- `IEvent.cs`
- `EventToken.cs`
- `EventBus.cs`
- `EventBusExtensions.cs`
- `Internal/*` (chi tiết triển khai, bạn thường không cần dùng trực tiếp)

---

## 1) IEvent.cs
Interface marker cho event struct.

**Ví dụ event**
```csharp
using HoangTuDongAnh.UP.Common.Patterns.Observer;

public readonly struct PlayerDiedEvent : IEvent
{
    public readonly int PlayerId;
    public PlayerDiedEvent(int playerId) => PlayerId = playerId;
}
```

---

## 2) Subscribe / Publish (EventBus.cs)

### Subscribe
```csharp
using HoangTuDongAnh.UP.Common.Patterns.Observer;

EventToken token = EventBus.Instance.Subscribe<PlayerDiedEvent>(e =>
{
    UnityEngine.Debug.Log($"Player died: {e.PlayerId}");
});
```

### Publish
```csharp
EventBus.Instance.Publish(new PlayerDiedEvent(1));
```

### Unsubscribe
```csharp
token.Dispose(); // hoặc EventBus.Instance.Unsubscribe(token)
```

---

## 3) Owner-based auto cleanup
Bạn có thể đăng ký listener gắn với `UnityEngine.Object owner` để khi owner bị destroy, bus có thể cleanup.

Ví dụ (tuỳ phiên bản API bạn đang dùng trong bus):
```csharp
EventBus.Instance.Subscribe<PlayerDiedEvent>(this, e => { ... });
```

---

## 4) EventBusExtensions.cs
Chứa các helper extension để đăng ký/unregister tiện hơn, giảm boilerplate.

---

## Best practices
- Event struct nên nhỏ gọn, immutable
- Tránh publish event quá dày (mỗi frame) nếu không cần
- Với UI: subscribe ở `OnEnable` và dispose ở `OnDisable` (hoặc dùng token + owner)
