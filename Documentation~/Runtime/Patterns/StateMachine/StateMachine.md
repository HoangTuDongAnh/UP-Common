# StateMachine.cs

**Mô tả:** /// Typed state machine with transitions. /// - Any-state transitions /// - Priority-based transitions /// - Timed transitions (StateTime) /// - Optional registry (key -> state)

**Đường dẫn:** `Runtime/Patterns/StateMachine/StateMachine.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.StateMachine`

## Kiểu dữ liệu trong file
- `public class StateMachine<TContext>`

## Public/Protected API (tự trích)
### Methods
- `public void RegisterState(string key, IState<TContext> state)`
- `public bool ChangeState(string key, bool forceReenter = false)`
- `public bool TryGetState(string key, out IState<TContext> state)`
- `public void Initialize(IState<TContext> initialState, bool forceReenter = true)`
- `public void AddTransition(IState<TContext> from, IState<TContext> to, Func<TContext, bool> condition, int priority = 0)`
- `public void AddAnyTransition(IState<TContext> to, Func<TContext, bool> condition, int priority = 0)`
- `public void AddTimedTransition(IState<TContext> from, IState<TContext> to, float seconds, int priority = 0)`
- `public bool RevertToPrevious(bool forceReenter = false)`
- `public bool ClearState()`
- `public void Tick()`
- `public void FixedTick()`
- `public void LateTick()`

### Properties
- `public IState<TContext> CurrentState`
- `public IState<TContext> PreviousState`
- `public bool EnableDebugLog`
- `public string DebugTag`

### Fields
- `public float StateTime`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
