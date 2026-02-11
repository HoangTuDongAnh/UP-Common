# Runtime/Utilities/IO

## Mục tiêu
Helper IO nhẹ:
- json serialize
- read/write text
- path helpers (persistentDataPath)

## Scripts
- `FileUtilLite.cs`
- `JsonUtil.cs`
- `PersistentPathUtil.cs`

## Ví dụ
```csharp
var path = Path.Combine(Application.persistentDataPath, "save.json");
File.WriteAllText(path, json);
```

> Với save system phức tạp: nên tách module riêng, IO util chỉ hỗ trợ thao tác cơ bản.
