# Runtime/Patterns/Singleton

## Mục tiêu
Cung cấp 2 loại singleton:
- `Singleton<T>`: singleton thuần C# (không cần MonoBehaviour)
- `MonoSingleton<T>`: singleton cho MonoBehaviour (tự đảm bảo chỉ có 1 instance)

Ngoài ra có cơ chế **reset static khi Enter Play Mode (SubsystemRegistration)** để hỗ trợ trường hợp **Domain Reload Off**.

## Scripts
- `Singleton.cs`
- `MonoSingleton.cs`

---

## 1) Singleton<T> (Singleton.cs)

### Khi nào dùng?
- Manager thuần logic không cần GameObject (ConfigManager, SaveManager…)
- Không phụ thuộc scene lifecycle

### Cách dùng nhanh
```csharp
public sealed class ConfigManager : Singleton<ConfigManager>
{
    public string Version = "1.0";
}

// dùng:
var v = ConfigManager.Instance.Version;
```

### Hành vi chính
- Lazy init: tạo instance khi `Instance` được gọi lần đầu
- Có cờ `Shutdown()` để chặn tạo instance khi app đang quit (tránh ghost init)

---

## 2) MonoSingleton<T> (MonoSingleton.cs)

### Khi nào dùng?
- Manager cần MonoBehaviour (coroutine, update, access Unity API…)
- Muốn tự tạo GameObject nếu chưa có instance trong scene

### Cách dùng nhanh
```csharp
public sealed class AudioManager : MonoSingleton<AudioManager>
{
    protected override bool ShouldPersistAcrossScenes => true;
}
```

### Hành vi chính
- Tự tìm instance trong scene, nếu không có thì tạo GameObject `T (Singleton)`
- Nếu có nhiều instance -> tự destroy bản thừa
- `ShouldPersistAcrossScenes` cho phép `DontDestroyOnLoad`

---

## 3) Reset statics (quan trọng)
Unity **không cho** `[RuntimeInitializeOnLoadMethod]` đặt trong generic class, nên UP-Common dùng entry non-generic:
- `Runtime/Utilities/Safety/RuntimeStaticReset.cs` gọi:
  - `SingletonRuntime.Reset()`
  - `MonoSingletonRuntime.Reset()`

### Khi nào cần?
- Khi bật `Enter Play Mode Options` và tắt Domain Reload
- Tránh static bị giữ lại giữa các lần Play

---

## Best practices
- Tránh lạm dụng singleton cho mọi thứ (nên dùng cho “dịch vụ”/manager)
- Singleton không nên giữ reference nặng tới scene object (để tránh leak)
