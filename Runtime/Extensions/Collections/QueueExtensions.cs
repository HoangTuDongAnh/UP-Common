using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// Queue helpers.
    /// </summary>
    public static class QueueExtensions
    {
        public static bool IsNullOrEmpty<T>(this Queue<T> q) => q == null || q.Count == 0;

        public static bool TryDequeue<T>(this Queue<T> q, out T item)
        {
            if (q != null && q.Count > 0)
            {
                item = q.Dequeue();
                return true;
            }

            item = default;
            return false;
        }

        public static bool TryPeek<T>(this Queue<T> q, out T item)
        {
            if (q != null && q.Count > 0)
            {
                item = q.Peek();
                return true;
            }

            item = default;
            return false;
        }
    }
}