using System.Collections.Generic;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Time
{
    /// <summary>
    /// Manage timeScale overrides (pause/slow motion).
    /// Use Push/Pop with a token owner object.
    /// </summary>
    public static class TimeScaleStack
    {
        private struct Entry
        {
            public object Owner;
            public float Scale;
        }

        private static readonly List<Entry> _stack = new List<Entry>(8);
        private static float _defaultScale = 1f;

        public static void SetDefault(float scale)
        {
            _defaultScale = Mathf.Clamp(scale, 0f, 10f);
            Recalculate();
        }

        public static void Push(object owner, float scale)
        {
            if (owner == null) return;
            scale = Mathf.Clamp(scale, 0f, 10f);

            _stack.Add(new Entry { Owner = owner, Scale = scale });
            Recalculate();
        }

        public static void Pop(object owner)
        {
            if (owner == null) return;

            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                if (ReferenceEquals(_stack[i].Owner, owner))
                {
                    _stack.RemoveAt(i);
                    break;
                }
            }

            Recalculate();
        }

        public static void Clear()
        {
            _stack.Clear();
            Recalculate();
        }

        private static void Recalculate()
        {
            float scale = _defaultScale;

            // Last push wins
            if (_stack.Count > 0)
                scale = _stack[_stack.Count - 1].Scale;

            UnityEngine.Time.timeScale = scale;
        }
    }
}