using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Vector2 helpers.
    /// </summary>
    public static class Vector2Extensions
    {
        public static Vector2 WithX(this Vector2 v, float x) => new Vector2(x, v.y);
        public static Vector2 WithY(this Vector2 v, float y) => new Vector2(v.x, y);

        public static Vector2 AddX(this Vector2 v, float dx) => new Vector2(v.x + dx, v.y);
        public static Vector2 AddY(this Vector2 v, float dy) => new Vector2(v.x, v.y + dy);

        public static float SqrDistanceTo(this Vector2 a, Vector2 b) => (a - b).sqrMagnitude;
        public static float DistanceTo(this Vector2 a, Vector2 b) => Vector2.Distance(a, b);
    }
}