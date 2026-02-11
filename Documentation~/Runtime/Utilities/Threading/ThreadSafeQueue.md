# ThreadSafeQueue.cs

**Mô tả:** /// Minimal thread-safe queue.

**Đường dẫn:** `Runtime/Utilities/Threading/ThreadSafeQueue.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Utilities.Threading`

## Kiểu dữ liệu trong file
- `public class ThreadSafeQueue<T>`

## Public/Protected API (tự trích)
### Methods
- `public void Enqueue(T item)`
- `public bool TryDequeue(out T item)`
- `public void Clear()`

### Properties
- `public int Count`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
