using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Utilities.Services
{
    /// <summary>
    /// Lightweight service locator.
    /// Use for small/medium projects.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>(32);

        public static void Register<T>(T service) where T : class
        {
            if (service == null) return;
            _services[typeof(T)] = service;
        }

        public static bool TryResolve<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = obj as T;
                return service != null;
            }

            service = null;
            return false;
        }

        public static T Resolve<T>() where T : class
        {
            if (TryResolve<T>(out var s)) return s;
            throw new InvalidOperationException($"Service not registered: {typeof(T).Name}");
        }

        public static void Unregister<T>() where T : class
        {
            _services.Remove(typeof(T));
        }

        public static void Clear()
        {
            _services.Clear();
        }
    }
}