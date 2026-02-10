using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Extensions.Collections
{
    /// <summary>
    /// Dictionary helpers.
    /// </summary>
    public static class DictionaryExtensions
    {
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict)
            => dict == null || dict.Count == 0;

        /// <summary>
        /// Get value or default if key not found.
        /// </summary>
        public static TValue GetOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dict,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dict == null) return defaultValue;
            return dict.TryGetValue(key, out var v) ? v : defaultValue;
        }

        /// <summary>
        /// Get existing value or create and add new one.
        /// </summary>
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dict,
            TKey key,
            Func<TValue> factory)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));

            if (dict.TryGetValue(key, out var v))
                return v;

            v = factory != null ? factory() : default(TValue);
            dict[key] = v;
            return v;
        }
    }
}