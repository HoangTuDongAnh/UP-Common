# Runtime/Extensions/Collections

## Mục tiêu
Helper cho `List`, `Dictionary`, `IEnumerable` để giảm boilerplate và code an toàn hơn.

## Scripts
- `ArrayExtensions.cs`
- `DictionaryExtensions.cs`
- `EnumerableExtensions.cs`
- `ListExtensions.cs`
- `QueueExtensions.cs`
- `StackExtensions.cs`

## Nội dung thường gặp
- TryGet / GetOrAdd / GetOrCreate
- AddRange / RemoveSwapBack
- Null/empty checks
- Foreach helper

## Ví dụ
```csharp
var v = dict.GetOrAdd(key, () => new List<int>());
```

> Ghi chú: tên method cụ thể phụ thuộc file hiện tại trong repo (bạn có thể search nhanh theo tên file).
