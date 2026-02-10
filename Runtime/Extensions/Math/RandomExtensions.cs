using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Random helpers.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Random bool with a true chance.
        /// </summary>
        public static bool Chance(float trueChance01)
        {
            if (trueChance01 <= 0f) return false;
            if (trueChance01 >= 1f) return true;
            return UnityEngine.Random.value < trueChance01;
        }

        /// <summary>
        /// Random sign: -1 or +1.
        /// </summary>
        public static int Sign() => UnityEngine.Random.value < 0.5f ? -1 : 1;

        /// <summary>
        /// Random point inside a circle (2D).
        /// </summary>
        public static Vector2 InsideCircle(float radius)
        {
            return UnityEngine.Random.insideUnitCircle * radius;
        }

        /// <summary>
        /// Random point inside a sphere (3D).
        /// </summary>
        public static Vector3 InsideSphere(float radius)
        {
            return UnityEngine.Random.insideUnitSphere * radius;
        }
    }
}