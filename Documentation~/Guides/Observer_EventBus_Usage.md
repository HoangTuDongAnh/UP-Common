# Hướng dẫn sử dụng: Observer (EventBus)

UP-Common dùng EventBus typed (event là struct) để giảm GC và rõ ràng hơn so với string event.

## 1) Tạo event
```csharp
using HoangTuDongAnh.UP.Common.Patterns.Observer;

public readonly struct PlayerDiedEvent : IEvent
{
    public readonly int PlayerId;
    public PlayerDiedEvent(int id) => PlayerId = id;
}
```

## 2) Subscribe
```csharp
EventToken token = EventBus.Instance.Subscribe<PlayerDiedEvent>(e =>
{
    UnityEngine.Debug.Log($"Player died: {e.PlayerId}");
});
```

## 3) Publish
```csharp
EventBus.Instance.Publish(new PlayerDiedEvent(1));
```

## 4) Unsubscribe
```csharp
token.Dispose();
```

## Best practices
- Subscribe ở `OnEnable`, dispose ở `OnDisable` (hoặc dùng owner-based subscribe nếu API hỗ trợ)
- Event struct nên nhỏ gọn, immutable
