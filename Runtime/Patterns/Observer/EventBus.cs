using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using HoangTuDongAnh.UP.Common.Patterns.Observer.Internal;
using HoangTuDongAnh.UP.Common.Patterns.Singleton;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Observer
{
    /// <summary>
    /// Typed event bus.
    /// - Token-based unsubscribe
    /// - Owner-based auto cleanup
    /// - Optional cross-thread publish (queued to main thread)
    /// </summary>
    public sealed class EventBus : MonoSingleton<EventBus>
    {
        private interface IChannel
        {
            void Unsubscribe(int id, int version);
            void UnregisterAll(UnityEngine.Object owner);
        }

        private sealed class ChannelWrapper<TEvent> : IChannel where TEvent : struct, IEvent
        {
            public readonly EventChannel<TEvent> Channel = new();
            public void Unsubscribe(int id, int version) => Channel.Unsubscribe(id, version);
            public void UnregisterAll(UnityEngine.Object owner) => Channel.UnregisterAll(owner);
        }

        private readonly Dictionary<Type, IChannel> _channels = new(64);
        private readonly ConcurrentQueue<Action> _mainThreadQueue = new();

        private ChannelWrapper<TEvent> GetChannel<TEvent>() where TEvent : struct, IEvent
        {
            var type = typeof(TEvent);
            if (_channels.TryGetValue(type, out var ch))
                return (ChannelWrapper<TEvent>)ch;

            var created = new ChannelWrapper<TEvent>();
            _channels.Add(type, created);
            return created;
        }

        /// <summary>
        /// Subscribe with owner (recommended).
        /// Owner destroyed => auto removed.
        /// </summary>
        public EventToken Subscribe<TEvent>(UnityEngine.Object owner, Action<TEvent> callback)
            where TEvent : struct, IEvent
        {
            var wrapper = GetChannel<TEvent>();
            return wrapper.Channel.Subscribe(owner, callback, (id, version) => wrapper.Unsubscribe(id, version));
        }

        /// <summary>
        /// Subscribe without owner (manual Dispose recommended).
        /// </summary>
        public EventToken Subscribe<TEvent>(Action<TEvent> callback)
            where TEvent : struct, IEvent
            => Subscribe<TEvent>(owner: null, callback);

        /// <summary>
        /// Publish now (main thread).
        /// </summary>
        public void Publish<TEvent>(in TEvent ev)
            where TEvent : struct, IEvent
        {
            GetChannel<TEvent>().Channel.Publish(ev);
        }

        /// <summary>
        /// Publish from any thread (queued to main thread).
        /// </summary>
        public void PublishFromAnyThread<TEvent>(TEvent ev)
            where TEvent : struct, IEvent
        {
            _mainThreadQueue.Enqueue(() => Publish(ev));
        }

        /// <summary>
        /// Remove all listeners owned by this object.
        /// </summary>
        public void UnregisterAll(UnityEngine.Object owner)
        {
            if (owner == null) return;
            foreach (var ch in _channels.Values)
                ch.UnregisterAll(owner);
        }

        /// <summary>
        /// Clear all listeners and queued events.
        /// </summary>
        public void ClearAll()
        {
            _channels.Clear();
            while (_mainThreadQueue.TryDequeue(out _)) { }
        }

        private void Update()
        {
            // Flush queued events on main thread.
            while (_mainThreadQueue.TryDequeue(out var a))
                a?.Invoke();
        }
    }
}
