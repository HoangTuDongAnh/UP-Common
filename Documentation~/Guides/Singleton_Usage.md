# Hướng dẫn sử dụng: Singleton

File này tổng hợp cách dùng Singleton (C#) và MonoSingleton (MonoBehaviour).

## Singleton<T>
Dùng cho manager thuần C# (không cần GameObject).

```csharp
public sealed class ConfigManager : Singleton<ConfigManager>
{
    public string Version = "1.0";
}

// sử dụng:
var v = ConfigManager.Instance.Version;
```

## MonoSingleton<T>
Dùng cho manager cần MonoBehaviour (coroutine/update).

```csharp
public sealed class AudioManager : MonoSingleton<AudioManager>
{
    protected override bool ShouldPersistAcrossScenes => true;
}
```

## Lưu ý
- Tránh lạm dụng singleton cho mọi thứ
- Singleton không nên giữ reference nặng tới scene object nếu không cần
- Khi bật Domain Reload Off, cần cơ chế reset static (UP-Common đã có trong Utilities/Safety)
