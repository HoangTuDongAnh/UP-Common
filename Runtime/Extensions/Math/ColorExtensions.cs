using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Color helpers.
    /// </summary>
    public static class ColorExtensions
    {
        public static Color WithA(this Color c, float a)
        {
            c.a = Mathf.Clamp01(a);
            return c;
        }

        public static string ToHtmlRGBA(this Color c)
        {
            return ColorUtility.ToHtmlStringRGBA(c);
        }

        public static bool TryParseHtml(string html, out Color color)
        {
            return ColorUtility.TryParseHtmlString(html, out color);
        }
    }
}