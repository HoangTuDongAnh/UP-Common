using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Utilities.Threading
{
    /// <summary>
    /// Minimal thread-safe queue.
    /// </summary>
    public sealed class ThreadSafeQueue<T>
    {
        private readonly Queue<T> _queue = new Queue<T>(64);
        private readonly object _lock = new object();

        public int Count
        {
            get { lock (_lock) return _queue.Count; }
        }

        public void Enqueue(T item)
        {
            lock (_lock) _queue.Enqueue(item);
        }

        public bool TryDequeue(out T item)
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    item = _queue.Dequeue();
                    return true;
                }
            }

            item = default(T);
            return false;
        }

        public void Clear()
        {
            lock (_lock) _queue.Clear();
        }
    }
}