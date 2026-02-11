# Runtime/Utilities/Data

## Mục tiêu
Cung cấp kiểu dữ liệu tiện cho binding / reactive nhẹ.

## Scripts
- `ObservableValue.cs`
- `ReactiveList.cs`

## Ví dụ (ObservableValue)
```csharp
var hp = new ObservableValue<int>(100);
hp.OnChanged += v => UpdateUI(v);
hp.Value -= 10;
```
