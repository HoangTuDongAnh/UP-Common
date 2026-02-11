namespace HoangTuDongAnh.UP.Common.Patterns.Singleton
{
    /// <summary>
    /// Non-generic runtime reset entry for Singleton<T>.
    /// </summary>
    internal static class SingletonRuntime
    {
        internal static void Reset()
        {
            SingletonBase.ResetAll();
        }
    }

    /// <summary>
    /// Non-generic base used to reset static fields for generic singletons.
    /// </summary>
    public abstract class SingletonBase
    {
        private static readonly System.Collections.Generic.List<System.Action> _resetters
            = new System.Collections.Generic.List<System.Action>(32);

        protected static void RegisterReset(System.Action reset)
        {
            if (reset == null) return;
            _resetters.Add(reset);
        }

        internal static void ResetAll()
        {
            for (int i = _resetters.Count - 1; i >= 0; i--)
            {
                try { _resetters[i]?.Invoke(); }
                catch { /* ignore */ }
            }
        }
    }

    /// <summary>
    /// Generic singleton (pure C#).
    /// </summary>
    public abstract class Singleton<T> : SingletonBase where T : class, new()
    {
        private static T _instance;
        private static bool _isQuitting;
        private static bool _resetRegistered;

        protected Singleton()
        {
            EnsureResetRegistered();
        }

        private static void EnsureResetRegistered()
        {
            if (_resetRegistered) return;
            _resetRegistered = true;

            RegisterReset(() =>
            {
                _instance = null;
                _isQuitting = false;
                _resetRegistered = false;
            });
        }

        public static T Instance
        {
            get
            {
                if (_isQuitting) return null;

                if (_instance == null)
                {
                    EnsureResetRegistered();
                    _instance = new T();
                }

                return _instance;
            }
        }

        public static bool HasInstance => _instance != null;

        /// <summary>
        /// Prevent creating new instances while quitting.
        /// </summary>
        public static void Shutdown() => _isQuitting = true;
    }
}
