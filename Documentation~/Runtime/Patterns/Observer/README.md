# Runtime/Patterns/Observer

Chi tiết module `Observer`.

## Danh sách script
| Script | Mô tả ngắn |
|---|---|
| [EventBus.cs](Runtime/Patterns/Observer/EventBus.md) | /// Typed event bus. /// - Token-based unsubscribe /// - Owner-based auto cleanup /// - Optional cross-thread publish (queued to main thread) |
| [EventBusExtensions.cs](Runtime/Patterns/Observer/EventBusExtensions.md) | /// MonoBehaviour shortcuts. |
| [EventToken.cs](Runtime/Patterns/Observer/EventToken.md) | /// Subscription handle. Dispose() to unsubscribe. |
| [IEvent.cs](Runtime/Patterns/Observer/IEvent.md) | /// Marker interface for typed events. /// Prefer struct events for low allocations. |
