# ReactiveList.cs

**Mô tả:** /// Small observable list for UI updates.

**Đường dẫn:** `Runtime/Utilities/Data/ReactiveList.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Utilities.Data`

## Kiểu dữ liệu trong file
- `public class ReactiveList<T>`

## Public/Protected API (tự trích)
### Methods
- `public void Add(T item)`
- `public bool Remove(T item)`
- `public void Clear()`

### Fields
- `public int Count`
- `public List<T> RawList`

### Events
- `public event Action OnChanged`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
