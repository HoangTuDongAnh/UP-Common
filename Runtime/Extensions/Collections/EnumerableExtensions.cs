using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// Simple enumerable helpers (no LINQ).
    /// </summary>
    public static class EnumerableExtensions
    {
        public static bool Any<T>(this IEnumerable<T> source)
        {
            if (source == null) return false;
            using var e = source.GetEnumerator();
            return e.MoveNext();
        }

        public static int CountSafe<T>(this IEnumerable<T> source)
        {
            if (source == null) return 0;
            int count = 0;
            foreach (var _ in source) count++;
            return count;
        }
    }
}