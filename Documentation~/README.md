# UP-Common — Tài liệu sử dụng

> Bản tài liệu này được trình bày **theo cấu trúc thư mục** của package.
> Mục tiêu: bạn mở repo lên là tra được **folder nào chứa gì**, **mỗi script dùng để làm gì**, và **cách dùng nhanh**.

- Ngày tạo docs: **2026-02-11**
- Package: **HoangTuDongAnh.UP.Common (UP-Common)**

---

## Mục lục

### Runtime
- [Runtime/Patterns](Runtime/Patterns/README.md)
- [Runtime/Extensions](Runtime/Extensions/README.md)
- [Runtime/Utilities](Runtime/Utilities/README.md)
- [Runtime/Attributes](Runtime/Attributes/README.md)

### Editor
- [Editor/PropertyDrawers](Editor/PropertyDrawers/README.md)
- [Editor/Tools](Editor/Tools/README.md)
- [Editor/Generators](Editor/Generators/README.md)
- [Editor/Settings](Editor/Settings/README.md)

---

## Quy ước chung

### 1) Tách Runtime và Editor
- Tất cả code chạy game nằm trong `Runtime/`
- Tất cả công cụ Unity Editor nằm trong `Editor/`
- **Runtime không được reference `UnityEditor`** (để build không lỗi)

### 2) Namespace
- Runtime: `HoangTuDongAnh.UP.Common.*`
- Editor: `HoangTuDongAnh.UP.Common.Editor.*`
- Tránh đặt namespace kiểu `Editor.*` (dễ xung đột và gây lỗi assembly)

### 3) Khi nào dùng Patterns / Extensions / Utilities?
- **Patterns**: kiến trúc & cách tổ chức code (Singleton/Observer/Command/StateMachine/Pooling)
- **Extensions**: helper tĩnh, không state, dùng mọi nơi
- **Utilities**: dịch vụ/tiện ích có state nhẹ (logging, timer, thread, service locator…)

---

## Gợi ý triển khai trong dự án
- Với dự án nhỏ/vừa: bạn có thể dùng trực tiếp các module trong `Runtime/Patterns` và `Runtime/Utilities`
- Với dự án lớn: nên giữ API ổn định và bổ sung layer riêng ở project (đừng sửa trực tiếp package)

