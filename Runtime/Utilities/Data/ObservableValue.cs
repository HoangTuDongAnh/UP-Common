using System;

namespace HoangTuDongAnh.UP.Common.Utilities.Data
{
    /// <summary>
    /// A lightweight observable value.
    /// Useful for UI binding without heavy frameworks.
    /// </summary>
    public sealed class ObservableValue<T>
    {
        public event Action<T> OnChanged;

        private T _value;

        public ObservableValue(T initialValue = default(T))
        {
            _value = initialValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }
    }
}