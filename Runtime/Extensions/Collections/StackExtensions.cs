using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// Stack helpers.
    /// </summary>
    public static class StackExtensions
    {
        public static bool IsNullOrEmpty<T>(this Stack<T> stack)
            => stack == null || stack.Count == 0;

        public static bool TryPop<T>(this Stack<T> stack, out T item)
        {
            if (stack != null && stack.Count > 0)
            {
                item = stack.Pop();
                return true;
            }

            item = default(T);
            return false;
        }

        public static bool TryPeek<T>(this Stack<T> stack, out T item)
        {
            if (stack != null && stack.Count > 0)
            {
                item = stack.Peek();
                return true;
            }

            item = default(T);
            return false;
        }
    }
}