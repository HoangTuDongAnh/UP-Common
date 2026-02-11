# Runtime/Patterns

Các pattern dùng để chuẩn hoá kiến trúc và workflow code.  
Tất cả pattern trong UP-Common được thiết kế để **dùng tốt cho dự án nhỏ & vừa**, dễ mở rộng và ít phụ thuộc lẫn nhau.

## Các module
- `Singleton/`: singleton cho C# và MonoBehaviour
- `Observer/`: event bus typed (publish/subscribe)
- `Command/`: command + invoker (+ undoable)
- `StateMachine/`: FSM đơn giản + transition
- `Pooling/`: object pool manager
