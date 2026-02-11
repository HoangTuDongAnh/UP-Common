# Runtime/Utilities/Logging

## Mục tiêu
- Chuẩn hoá logging trong project
- Có mức log (Info/Warn/Error…)
- Có scope đo thời gian block code

## Scripts
- `Log.cs`
- `LogLevel.cs`
- `LogScope.cs`

## 1) Log.cs
- API log trung tâm.
- Thường hỗ trợ `Log.Level` để giới hạn log.

**Ví dụ**
```csharp
using HoangTuDongAnh.UP.Common.Utilities.Logging;

Log.Info("Hello");
Log.Warn("Warning");
Log.Error("Error");
```

## 2) LogScope.cs
Dùng để đo thời gian chạy một đoạn code theo pattern `using`.

```csharp
using (new LogScope("LoadConfig"))
{
    LoadConfig();
}
```

### Lưu ý quan trọng
Do package có namespace `...Utilities.Time`, khi dùng Unity time bạn nên gọi:
```csharp
UnityEngine.Time.realtimeSinceStartup
```
(để tránh bị shadow bởi namespace Time)
