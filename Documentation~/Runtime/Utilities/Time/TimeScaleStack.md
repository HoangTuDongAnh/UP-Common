# TimeScaleStack.cs

**Mô tả:** /// Manage timeScale overrides (pause/slow motion). /// Use Push/Pop with a token owner object.

**Đường dẫn:** `Runtime/Utilities/Time/TimeScaleStack.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Utilities.Time`

## Kiểu dữ liệu trong file
- `public class TimeScaleStack`
- `private struct Entry`

## Public/Protected API (tự trích)
### Methods
- `public void SetDefault(float scale)`
- `public void Push(object owner, float scale)`
- `public void Pop(object owner)`
- `public void Clear()`

### Fields
- `public object Owner`
- `public float Scale`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
