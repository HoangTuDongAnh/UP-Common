# Runtime/Patterns/Command

## Mục tiêu
Triển khai Command Pattern để:
- tách “yêu cầu hành động” khỏi “người thực thi”
- hỗ trợ queue/execute
- dễ mở rộng undo/redo (nếu muốn)

## Scripts
- `ICommand.cs`
- `CommandInvoker.cs`
- (nếu có) `IUndoableCommand.cs` / các command concrete trong project của bạn

---

## 1) ICommand.cs
Interface cơ bản của command.

**Ví dụ**
```csharp
public sealed class SpawnEnemyCommand : ICommand
{
    public void Execute()
    {
        // spawn enemy
    }
}
```

## 2) CommandInvoker.cs
- Nhận command và execute
- Có thể quản lý queue / history (tuỳ implement hiện tại trong repo)

**Ví dụ**
```csharp
var invoker = new CommandInvoker();
invoker.Execute(new SpawnEnemyCommand());
```

## Best practices
- Command nên nhỏ, làm 1 việc rõ ràng
- Nếu cần undo: tách interface `IUndoableCommand` (Execute/Undo) và invoker quản lý stack
