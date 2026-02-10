using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Bounds helpers.
    /// </summary>
    public static class BoundsExtensions
    {
        public static Vector3 RandomPoint(this Bounds b)
        {
            var min = b.min;
            var max = b.max;

            return new Vector3(
                UnityEngine.Random.Range(min.x, max.x),
                UnityEngine.Random.Range(min.y, max.y),
                UnityEngine.Random.Range(min.z, max.z)
            );
        }

        public static bool ContainsXZ(this Bounds b, Vector3 worldPos)
        {
            return worldPos.x >= b.min.x && worldPos.x <= b.max.x &&
                   worldPos.z >= b.min.z && worldPos.z <= b.max.z;
        }
    }
}