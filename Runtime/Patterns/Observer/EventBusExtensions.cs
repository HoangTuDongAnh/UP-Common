using System;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Patterns.Observer
{
    /// <summary>
    /// MonoBehaviour shortcuts.
    /// </summary>
    public static class EventBusExtensions
    {
        public static EventToken Subscribe<TEvent>(this MonoBehaviour owner, Action<TEvent> callback)
            where TEvent : struct, IEvent
            => EventBus.Instance.Subscribe(owner, callback);

        public static void Publish<TEvent>(this MonoBehaviour sender, in TEvent ev)
            where TEvent : struct, IEvent
            => EventBus.Instance.Publish(ev);

        public static void PublishFromAnyThread<TEvent>(this MonoBehaviour sender, TEvent ev)
            where TEvent : struct, IEvent
            => EventBus.Instance.PublishFromAnyThread(ev);

        public static void UnregisterAllEvents(this MonoBehaviour owner)
        {
            if (owner == null) return;
            EventBus.Instance.UnregisterAll(owner);
        }
    }
}