# Runtime/Patterns/StateMachine

Chi tiết module `StateMachine`.

## Danh sách script
| Script | Mô tả ngắn |
|---|---|
| [IState.cs](Runtime/Patterns/StateMachine/IState.md) | /// Basic state (no context). |
| [StateMachine.cs](Runtime/Patterns/StateMachine/StateMachine.md) | /// Typed state machine with transitions. /// - Any-state transitions /// - Priority-based transitions /// - Timed transitions (StateTime) /// - Optional registry (key -> state) |
| [StateTransition.cs](Runtime/Patterns/StateMachine/StateTransition.md) | /// Conditional transition (From -> To). /// From == null means Any-State transition. |
