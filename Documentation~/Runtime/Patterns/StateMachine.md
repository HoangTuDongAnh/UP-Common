# Runtime/Patterns/StateMachine

## Mục tiêu
StateMachine giúp:
- quản lý logic theo trạng thái (idle/run/attack…)
- tách code theo state -> dễ đọc, dễ test
- hỗ trợ transition rõ ràng

## Scripts
- `IState.cs`
- `StateMachine.cs`
- `StateTransition.cs` (nếu có trong bản hiện tại)

---

## 1) IState.cs
Định nghĩa contract cho state:
- Enter / Exit
- Tick (Update)
- (tuỳ phiên bản) FixedTick / LateTick

## 2) StateMachine.cs
- giữ state hiện tại
- ChangeState
- Tick

**Ví dụ**
```csharp
public sealed class IdleState : IState
{
    public void Enter() { }
    public void Exit() { }
    public void Tick(float dt) { }
}

var fsm = new StateMachine();
fsm.ChangeState(new IdleState());
fsm.Tick(Time.deltaTime);
```

## 3) Transition (nếu dùng)
Nếu bạn có `StateTransition.cs`, bạn có thể cấu hình điều kiện chuyển state tách rời.

## Best practices
- State không nên tự tìm dependency (inject qua constructor)
- Update loop gọi `fsm.Tick(dt)` ở MonoBehaviour
