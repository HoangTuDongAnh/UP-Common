using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// RectTransform helpers (UI).
    /// </summary>
    public static class RectTransformExtensions
    {
        public static void SetAnchoredX(this RectTransform rt, float x)
        {
            if (rt == null) return;
            var p = rt.anchoredPosition;
            p.x = x;
            rt.anchoredPosition = p;
        }

        public static void SetAnchoredY(this RectTransform rt, float y)
        {
            if (rt == null) return;
            var p = rt.anchoredPosition;
            p.y = y;
            rt.anchoredPosition = p;
        }

        public static void StretchToFill(this RectTransform rt)
        {
            if (rt == null) return;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
    }
}