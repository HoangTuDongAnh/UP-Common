using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Vector3 helpers.
    /// Small, allocation-free utilities for common vector operations.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Return a copy of the vector with a new X value.
        /// </summary>
        public static Vector3 WithX(this Vector3 v, float x)
            => new Vector3(x, v.y, v.z);

        /// <summary>
        /// Return a copy of the vector with a new Y value.
        /// </summary>
        public static Vector3 WithY(this Vector3 v, float y)
            => new Vector3(v.x, y, v.z);

        /// <summary>
        /// Return a copy of the vector with a new Z value.
        /// </summary>
        public static Vector3 WithZ(this Vector3 v, float z)
            => new Vector3(v.x, v.y, z);

        /// <summary>
        /// Add delta to X and return a new vector.
        /// </summary>
        public static Vector3 AddX(this Vector3 v, float dx)
            => new Vector3(v.x + dx, v.y, v.z);

        /// <summary>
        /// Add delta to Y and return a new vector.
        /// </summary>
        public static Vector3 AddY(this Vector3 v, float dy)
            => new Vector3(v.x, v.y + dy, v.z);

        /// <summary>
        /// Add delta to Z and return a new vector.
        /// </summary>
        public static Vector3 AddZ(this Vector3 v, float dz)
            => new Vector3(v.x, v.y, v.z + dz);

        /// <summary>
        /// Squared distance to another vector (faster than Distance).
        /// </summary>
        public static float SqrDistanceTo(this Vector3 a, Vector3 b)
            => (a - b).sqrMagnitude;

        /// <summary>
        /// Distance to another vector.
        /// </summary>
        public static float DistanceTo(this Vector3 a, Vector3 b)
            => Vector3.Distance(a, b);

        /// <summary>
        /// Flatten Y to 0 (useful for ground-based movement).
        /// </summary>
        public static Vector3 FlatY(this Vector3 v)
            => new Vector3(v.x, 0f, v.z);

        /// <summary>
        /// Clamp vector magnitude. 
        /// Returns Vector3.zero if maxLength <= 0.
        /// </summary>
        public static Vector3 ClampMagnitude(this Vector3 v, float maxLength)
        {
            if (maxLength <= 0f) return Vector3.zero;
            return Vector3.ClampMagnitude(v, maxLength);
        }

        /// <summary>
        /// Project vector onto a plane defined by its normal.
        /// </summary>
        public static Vector3 ProjectOnPlane(this Vector3 v, Vector3 planeNormal)
            => Vector3.ProjectOnPlane(v, planeNormal);

        /// <summary>
        /// Get normalized direction vector from one point to another.
        /// Returns Vector3.zero if points are the same.
        /// </summary>
        public static Vector3 DirectionTo(this Vector3 from, Vector3 to)
        {
            var d = to - from;
            return d.sqrMagnitude > 0f ? d.normalized : Vector3.zero;
        }
    }
}
