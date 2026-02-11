# StateTransition.cs

**Mô tả:** /// Conditional transition (From -> To). /// From == null means Any-State transition.

**Đường dẫn:** `Runtime/Patterns/StateMachine/StateTransition.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.StateMachine`

## Kiểu dữ liệu trong file
- `public class StateTransition<TContext>`

## Public/Protected API (tự trích)
### Fields
- `public IState<TContext> From`
- `public IState<TContext> To`
- `public int Priority`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
