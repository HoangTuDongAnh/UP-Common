using System;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// Array helpers.
    /// Keep it allocation-free.
    /// </summary>
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] arr) => arr == null || arr.Length == 0;

        public static bool InBounds<T>(this T[] arr, int index)
            => arr != null && (uint)index < (uint)arr.Length;

        public static T GetOrDefault<T>(this T[] arr, int index, T defaultValue = default)
            => arr.InBounds(index) ? arr[index] : defaultValue;

        public static void ForEach<T>(this T[] arr, Action<T> action)
        {
            if (arr == null || action == null) return;
            for (int i = 0; i < arr.Length; i++)
                action(arr[i]);
        }
    }
}