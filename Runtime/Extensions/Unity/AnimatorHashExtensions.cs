using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Animator hashing helpers.
    /// Use hashes to avoid string cost in hot paths.
    /// </summary>
    public static class AnimatorHashExtensions
    {
        public static int ToAnimatorHash(this string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;
            return Animator.StringToHash(name);
        }

        public static void SetBoolSafe(this Animator animator, int hash, bool value)
        {
            if (animator == null || hash == 0) return;
            animator.SetBool(hash, value);
        }

        public static void SetTriggerSafe(this Animator animator, int hash)
        {
            if (animator == null || hash == 0) return;
            animator.SetTrigger(hash);
        }
    }
}