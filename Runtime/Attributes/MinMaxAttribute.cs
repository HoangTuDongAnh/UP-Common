using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Attributes
{
    /// <summary>
    /// Draw Vector2 as min-max slider.
    /// </summary>
    public sealed class MinMaxAttribute : PropertyAttribute
    {
        public float Min;
        public float Max;

        public MinMaxAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}