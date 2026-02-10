using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Float helpers.
    /// </summary>
    public static class FloatExtensions
    {
        public static float Clamp01(this float v) => Mathf.Clamp01(v);
        public static float Clamp(this float v, float min, float max) => Mathf.Clamp(v, min, max);

        public static bool Approximately(this float a, float b, float tolerance = 0.0001f)
            => Mathf.Abs(a - b) <= tolerance;

        public static float Remap(this float v, float inMin, float inMax, float outMin, float outMax)
        {
            if (Mathf.Approximately(inMin, inMax)) return outMin;
            float t = (v - inMin) / (inMax - inMin);
            return Mathf.Lerp(outMin, outMax, t);
        }
    }
}