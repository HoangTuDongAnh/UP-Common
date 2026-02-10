using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Animator helpers.
    /// </summary>
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Play state safely (null-safe).
        /// </summary>
        public static void PlaySafe(this Animator animator, string stateName, int layer = 0, float normalizedTime = 0f)
        {
            if (animator == null || string.IsNullOrEmpty(stateName)) return;
            animator.Play(stateName, layer, normalizedTime);
        }

        /// <summary>
        /// Set bool parameter safely.
        /// </summary>
        public static void SetBoolSafe(this Animator animator, string param, bool value)
        {
            if (animator == null || string.IsNullOrEmpty(param)) return;
            animator.SetBool(param, value);
        }

        /// <summary>
        /// Set trigger parameter safely.
        /// </summary>
        public static void SetTriggerSafe(this Animator animator, string param)
        {
            if (animator == null || string.IsNullOrEmpty(param)) return;
            animator.SetTrigger(param);
        }

        /// <summary>
        /// Check if animator is currently in a specific state.
        /// </summary>
        public static bool IsInState(this Animator animator, string stateName, int layer = 0)
        {
            if (animator == null || string.IsNullOrEmpty(stateName)) return false;
            return animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName);
        }

        /// <summary>
        /// Get normalized time of current state (can exceed 1 for looping states).
        /// </summary>
        public static float GetNormalizedTime(this Animator animator, int layer = 0)
        {
            if (animator == null) return 0f;
            return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;
        }
    }
}