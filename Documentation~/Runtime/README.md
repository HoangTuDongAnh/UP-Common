# Runtime

`Runtime/` chứa toàn bộ code dùng trong game (được build vào player).

## Nhóm chính
- **Attributes**: attribute dùng ở runtime (drawer nằm trong Editor)
- **Patterns**: các design pattern (Singleton/Observer/Command/StateMachine/Pooling)
- **Extensions**: extension methods (helper, không state)
- **Utilities**: tiện ích & dịch vụ dùng chung (logging, time, threading, services, IO...)

## Lưu ý
- Runtime không được `using UnityEditor;`
- Mọi thứ trong Runtime nên tối ưu GC và tránh allocation không cần thiết.
