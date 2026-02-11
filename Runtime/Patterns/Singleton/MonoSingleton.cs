using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Singleton
{
    /// <summary>
    /// Non-generic runtime reset entry for MonoSingleton<T>.
    /// </summary>
    internal static class MonoSingletonRuntime
    {
        private static readonly System.Collections.Generic.List<System.Action> _resetters
            = new System.Collections.Generic.List<System.Action>(32);

        internal static void RegisterReset(System.Action reset)
        {
            if (reset == null) return;
            _resetters.Add(reset);
        }

        internal static void Reset()
        {
            for (int i = _resetters.Count - 1; i >= 0; i--)
            {
                try { _resetters[i]?.Invoke(); }
                catch { /* ignore */ }
            }
        }
    }

    /// <summary>
    /// MonoBehaviour singleton.
    /// - Finds existing instance, or creates one.
    /// - Optional persistent across scenes.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _isQuitting;
        private static bool _resetRegistered;

        /// <summary>
        /// Override if you don't want DontDestroyOnLoad.
        /// </summary>
        protected virtual bool ShouldPersistAcrossScenes => true;

        /// <summary>
        /// Global access to the singleton instance.
        /// Returns null while quitting (avoid ghost objects).
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isQuitting) return null;

                lock (_lock)
                {
                    if (_instance != null) return _instance;

                    EnsureResetRegistered();

#if UNITY_2022_2_OR_NEWER
                    _instance = Object.FindFirstObjectByType<T>();
#else
                    _instance = Object.FindObjectOfType<T>();
#endif

                    if (_instance == null)
                    {
                        var go = new GameObject($"{typeof(T).Name} (Singleton)");
                        _instance = go.AddComponent<T>();

                        if (_instance.ShouldPersistAcrossScenes)
                            DontDestroyOnLoad(go);
                    }

                    return _instance;
                }
            }
        }

        public static bool HasInstance => _instance != null;

        private static void EnsureResetRegistered()
        {
            if (_resetRegistered) return;
            _resetRegistered = true;

            MonoSingletonRuntime.RegisterReset(() =>
            {
                _instance = null;
                _isQuitting = false;
                _resetRegistered = false;
            });
        }

        /// <summary>
        /// Ensures only one instance exists.
        /// </summary>
        protected virtual void Awake()
        {
            EnsureResetRegistered();

            if (_instance == null)
            {
                _instance = (T)this;

                if (ShouldPersistAcrossScenes)
                    DontDestroyOnLoad(gameObject);

                return;
            }

            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Marks quitting to prevent new instance creation.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        /// <summary>
        /// Clear instance if this object is the current singleton.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
