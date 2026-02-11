using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Threading
{
    /// <summary>
    /// Run actions on Unity main thread.
    /// </summary>
    public sealed class MainThreadDispatcher : MonoBehaviour
    {
        private static MainThreadDispatcher _instance;

        private readonly Queue<Action> _queue = new Queue<Action>(64);
        private readonly object _lock = new object();

        public static void Ensure()
        {
            if (_instance != null) return;

            var go = new GameObject("[MainThreadDispatcher]");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<MainThreadDispatcher>();
        }

        public static void Post(Action action)
        {
            if (action == null) return;
            Ensure();

            lock (_instance._lock)
            {
                _instance._queue.Enqueue(action);
            }
        }

        private void Update()
        {
            // Drain queue
            while (true)
            {
                Action a = null;

                lock (_lock)
                {
                    if (_queue.Count > 0)
                        a = _queue.Dequeue();
                }

                if (a == null) break;

                try { a.Invoke(); }
                catch (Exception e) { Debug.LogException(e); }
            }
        }
    }
}