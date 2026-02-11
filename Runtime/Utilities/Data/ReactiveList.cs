using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Utilities.Data
{
    /// <summary>
    /// Small observable list for UI updates.
    /// </summary>
    public sealed class ReactiveList<T>
    {
        public event Action OnChanged;

        private readonly List<T> _list = new List<T>();

        public int Count => _list.Count;

        public T this[int index] => _list[index];

        public void Add(T item)
        {
            _list.Add(item);
            OnChanged?.Invoke();
        }

        public bool Remove(T item)
        {
            bool ok = _list.Remove(item);
            if (ok) OnChanged?.Invoke();
            return ok;
        }

        public void Clear()
        {
            if (_list.Count == 0) return;
            _list.Clear();
            OnChanged?.Invoke();
        }

        public List<T> RawList => _list; // use carefully
    }
}