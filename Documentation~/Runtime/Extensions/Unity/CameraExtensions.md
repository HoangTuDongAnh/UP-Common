# CameraExtensions.cs

**Mô tả:** /// Camera helpers.

**Đường dẫn:** `Runtime/Extensions/Unity/CameraExtensions.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Extensions.Unity`

## Kiểu dữ liệu trong file
- `public class CameraExtensions`

## Public/Protected API (tự trích)
### Methods
- `public Vector3 WorldToScreenPointSafe(this Camera cam, Vector3 worldPos)`
- `public Vector3 ScreenToWorldPointAtDistance(this Camera cam, Vector3 screenPos, float distance)`
- `public bool IsWorldPointVisible(this Camera cam, Vector3 worldPos, bool requireInFront = true)`
- `public Ray ScreenPointToRaySafe(this Camera cam, Vector3 screenPos)`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
