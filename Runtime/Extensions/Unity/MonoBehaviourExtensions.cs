using System;            // ✅ for Action
using System.Collections;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// MonoBehaviour helpers.
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Stop a coroutine safely (null-safe).
        /// </summary>
        public static void StopCoroutineSafe(this MonoBehaviour mb, Coroutine routine)
        {
            if (mb == null || routine == null) return;
            mb.StopCoroutine(routine);
        }

        /// <summary>
        /// Start coroutine safely (null-safe).
        /// </summary>
        public static Coroutine StartCoroutineSafe(this MonoBehaviour mb, IEnumerator routine)
        {
            if (mb == null || routine == null) return null;
            return mb.StartCoroutine(routine);
        }

        /// <summary>
        /// Invoke an action after delay using coroutine (no string Invoke).
        /// </summary>
        public static Coroutine InvokeDelayed(this MonoBehaviour mb, float delay, Action action)
        {
            if (mb == null || action == null) return null;
            return mb.StartCoroutine(InvokeDelayedRoutine(delay, action));
        }

        private static IEnumerator InvokeDelayedRoutine(float delay, Action action)
        {
            if (delay > 0f) yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}