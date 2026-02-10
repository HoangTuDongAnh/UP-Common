using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Observer.Internal
{
    /// <summary>
    /// Typed channel for a single event type.
    /// Internal: used by EventBus.
    /// </summary>
    internal sealed class EventChannel<TEvent> where TEvent : struct, IEvent
    {
        private struct Slot
        {
            public int Id;
            public int Version;
            public UnityEngine.Object Owner; // optional
            public Action<TEvent> Callback;
            public bool Active;
        }

        private readonly List<Slot> _slots = new(16);
        private readonly Stack<int> _free = new();
        private int _nextId = 1;

        public EventToken Subscribe(UnityEngine.Object owner, Action<TEvent> callback, Action<int, int> unsubscribeHook)
        {
            if (callback == null) return default;

            int index = _free.Count > 0 ? _free.Pop() : _slots.Count;
            var slot = (index < _slots.Count) ? _slots[index] : default;

            slot.Active = true;
            slot.Owner = owner;
            slot.Callback = callback;

            // New identity per subscription
            slot.Id = _nextId++;
            slot.Version = slot.Version + 1;

            if (index < _slots.Count) _slots[index] = slot;
            else _slots.Add(slot);

            return new EventToken(slot.Id, slot.Version, unsubscribeHook);
        }

        public void Unsubscribe(int id, int version)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                var s = _slots[i];
                if (!s.Active) continue;
                if (s.Id != id || s.Version != version) continue;

                s.Active = false;
                s.Owner = null;
                s.Callback = null;
                _slots[i] = s;
                _free.Push(i);
                return;
            }
        }

        public void UnregisterAll(UnityEngine.Object owner)
        {
            if (owner == null) return;

            for (int i = _slots.Count - 1; i >= 0; i--)
            {
                var s = _slots[i];
                if (!s.Active) continue;

                if (s.Owner == owner)
                {
                    s.Active = false;
                    s.Owner = null;
                    s.Callback = null;
                    _slots[i] = s;
                    _free.Push(i);
                }
            }
        }

        public void Publish(in TEvent ev)
        {
            for (int i = _slots.Count - 1; i >= 0; i--)
            {
                var s = _slots[i];
                if (!s.Active) continue;

                // Owner destroyed => auto remove
                if (s.Owner != null && s.Owner == null)
                {
                    s.Active = false;
                    s.Callback = null;
                    _slots[i] = s;
                    _free.Push(i);
                    continue;
                }

                try
                {
                    s.Callback?.Invoke(ev);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
    }
}
