using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Utilities.Safety
{
    /// <summary>
    /// Collect disposables and dispose them together.
    /// Great for event subscriptions.
    /// </summary>
    public sealed class DisposableGroup : IDisposable
    {
        private readonly List<IDisposable> _items = new List<IDisposable>(8);
        private bool _disposed;

        public void Add(IDisposable disposable)
        {
            if (_disposed) return;
            if (disposable == null) return;
            _items.Add(disposable);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            for (int i = _items.Count - 1; i >= 0; i--)
            {
                try { _items[i]?.Dispose(); }
                catch { /* ignore */ }
            }

            _items.Clear();
        }
    }
}