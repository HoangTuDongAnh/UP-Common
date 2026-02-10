using System;

namespace HoangTuDongAnh.UP.Common.Patterns.Singleton
{
    /// <summary>
    /// Singleton for plain C# classes (non-MonoBehaviour).
    /// - Thread-safe
    /// - Lazy initialization
    /// Note: requires public parameterless constructor.
    /// </summary>
    public abstract class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> _lazy = new Lazy<T>(() => new T(), true);

        /// <summary>
        /// Global access to the singleton instance.
        /// </summary>
        public static T Instance => _lazy.Value;

        /// <summary>
        /// True if Instance has been created.
        /// </summary>
        public static bool HasInstance => _lazy.IsValueCreated;

        /// <summary>
        /// Get instance if created, otherwise null.
        /// </summary>
        public static T TryGetInstance() => _lazy.IsValueCreated ? _lazy.Value : null;
    }
}