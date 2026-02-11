# AnimatorExtensions.cs

**Mô tả:** /// Animator helpers.

**Đường dẫn:** `Runtime/Extensions/Unity/AnimatorExtensions.cs`

**Namespace:** `HoangTuDongAnh.UP.Common.Extensions.Unity`

## Kiểu dữ liệu trong file
- `public class AnimatorExtensions`

## Public/Protected API (tự trích)
### Methods
- `public void PlaySafe(this Animator animator, string stateName, int layer = 0, float normalizedTime = 0f)`
- `public void SetBoolSafe(this Animator animator, string param, bool value)`
- `public void SetTriggerSafe(this Animator animator, string param)`
- `public bool IsInState(this Animator animator, string stateName, int layer = 0)`
- `public float GetNormalizedTime(this Animator animator, int layer = 0)`

> Ghi chú: Đây là danh sách trích tự động, có thể không bao gồm member private/internal, và có thể thiếu với các cú pháp C# đặc biệt.
