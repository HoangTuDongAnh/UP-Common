using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Singleton
{
    /// <summary>
    /// MonoBehaviour singleton.
    /// - Finds existing instance or creates one.
    /// - Optional DontDestroyOnLoad.
    /// - Safe with "Disable Domain Reload" play mode.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _isQuitting;

        /// <summary>
        /// True if an instance already exists.
        /// </summary>
        public static bool HasInstance => _instance != null;

        /// <summary>
        /// Get instance if exists, otherwise null (no auto-create).
        /// </summary>
        public static T TryGetInstance()
        {
            if (_isQuitting) return null;

#if UNITY_2022_2_OR_NEWER
            return _instance != null ? _instance : Object.FindFirstObjectByType<T>();
#else
            return _instance != null ? _instance : Object.FindObjectOfType<T>();
#endif
        }

        /// <summary>
        /// Global access (auto-create if needed).
        /// Returns null while quitting.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isQuitting) return null;

                lock (_lock)
                {
                    if (_instance != null) return _instance;

                    _instance = TryGetInstance();

                    if (_instance == null)
                    {
                        // Create a new GameObject if none exists.
                        var go = new GameObject($"{typeof(T).Name} (Singleton)");
                        _instance = go.AddComponent<T>();

                        if (_instance.ShouldPersistAcrossScenes)
                            DontDestroyOnLoad(go);
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Override to disable DontDestroyOnLoad.
        /// </summary>
        protected virtual bool ShouldPersistAcrossScenes => true;

        /// <summary>
        /// Ensures only one instance exists.
        /// </summary>
        protected virtual void Awake()
        {
            if (_isQuitting)
            {
                // Avoid resurrecting while quitting.
                Destroy(gameObject);
                return;
            }

            if (_instance == null)
            {
                _instance = (T)this;

                if (ShouldPersistAcrossScenes)
                    DontDestroyOnLoad(gameObject);

                OnSingletonAwake();
                return;
            }

            if (_instance != this)
            {
                // Keep the first instance, destroy duplicates.
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Optional hook after instance is assigned.
        /// </summary>
        protected virtual void OnSingletonAwake() { }

        /// <summary>
        /// Mark quitting to prevent new instance creation.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        /// <summary>
        /// Clear static ref when instance is destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        /// <summary>
        /// Reset static fields when domain reload is disabled.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _instance = null;
            _isQuitting = false;
        }
    }
}
