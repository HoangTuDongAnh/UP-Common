# CommandInvoker.cs

**Mô tả:** /// Executes commands and keeps history for Undo/Redo.

**Đường dẫn:** `Runtime/Patterns/Command/CommandInvoker.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Patterns.Command`

## Kiểu dữ liệu trong file
- `public class CommandInvoker`

## Public/Protected API (tự trích)
### Methods
- `public void Execute(ICommand command)`
- `public void Undo()`
- `public void Redo()`
- `public void ClearHistory()`

### Properties
- `public int MaxHistory`

### Fields
- `public int UndoCount`
- `public int RedoCount`
- `public bool CanUndo`
- `public bool CanRedo`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
