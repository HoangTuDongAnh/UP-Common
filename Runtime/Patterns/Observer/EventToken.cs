using System;

namespace HoangTuDongAnh.UP.Common.Patterns.Observer
{
    /// <summary>
    /// Subscription handle. Dispose() to unsubscribe.
    /// </summary>
    public readonly struct EventToken : IDisposable
    {
        private readonly int _id;
        private readonly int _version;
        private readonly Action<int, int> _unsubscribe;

        public bool IsValid => _unsubscribe != null;

        internal EventToken(int id, int version, Action<int, int> unsubscribe)
        {
            _id = id;
            _version = version;
            _unsubscribe = unsubscribe;
        }

        public void Dispose()
        {
            _unsubscribe?.Invoke(_id, _version);
        }
    }
}