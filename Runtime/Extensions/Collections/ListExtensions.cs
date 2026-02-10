using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// List helpers.
    /// </summary>
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;

        public static bool InBounds<T>(this List<T> list, int index)
            => list != null && (uint)index < (uint)list.Count;

        public static T GetOrDefault<T>(this List<T> list, int index, T defaultValue = default)
            => list.InBounds(index) ? list[index] : defaultValue;

        /// <summary>
        /// Swap two elements.
        /// </summary>
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            if (list == null) return;
            if (!list.InBounds(i) || !list.InBounds(j) || i == j) return;

            (list[i], list[j]) = (list[j], list[i]);
        }

        /// <summary>
        /// Remove by swapping with last element (O(1), order not preserved).
        /// </summary>
        public static bool RemoveSwapBack<T>(this List<T> list, int index)
        {
            if (list == null || !list.InBounds(index)) return false;

            int last = list.Count - 1;
            list[index] = list[last];
            list.RemoveAt(last);
            return true;
        }

        /// <summary>
        /// Add if item is not already in list.
        /// </summary>
        public static bool AddUnique<T>(this List<T> list, T item)
        {
            if (list == null) return false;
            if (list.Contains(item)) return false;

            list.Add(item);
            return true;
        }
    }
}