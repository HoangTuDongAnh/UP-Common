using System;
using System.Threading;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Threading
{
    /// <summary>
    /// Capture Unity main thread id and provide helpers.
    /// </summary>
    public static class UnitySyncContext
    {
        private static int _mainThreadId;
        private static bool _initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
            _initialized = true;

            MainThreadDispatcher.Ensure();
        }

        public static bool IsMainThread
            => _initialized && Thread.CurrentThread.ManagedThreadId == _mainThreadId;

        /// <summary>
        /// Run action now if on main thread, otherwise post to main thread.
        /// </summary>
        public static void RunOnMainThread(Action action)
        {
            if (action == null) return;

            if (IsMainThread) action.Invoke();
            else MainThreadDispatcher.Post(action);
        }
    }
}