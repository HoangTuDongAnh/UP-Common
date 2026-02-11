# Runtime/Utilities/Services

## Mục tiêu
Cung cấp Service Locator nhẹ cho dự án nhỏ/vừa:
- Register service
- Resolve service

## Scripts
- `ServiceInstaller.cs`
- `ServiceLocator.cs`

## Ví dụ
```csharp
ServiceLocator.Register<IAudioService>(new AudioService());
var audio = ServiceLocator.Resolve<IAudioService>();
audio.Play();
```

## Lưu ý
- ServiceLocator tiện nhưng không nên lạm dụng ở dự án rất lớn
- Nếu project lớn, cân nhắc DI container hoặc composition root riêng
