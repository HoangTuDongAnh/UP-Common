using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Quaternion helpers.
    /// </summary>
    public static class QuaternionExtensions
    {
        public static Quaternion WithEulerX(this Quaternion q, float x)
        {
            var e = q.eulerAngles;
            e.x = x;
            return Quaternion.Euler(e);
        }

        public static Quaternion WithEulerY(this Quaternion q, float y)
        {
            var e = q.eulerAngles;
            e.y = y;
            return Quaternion.Euler(e);
        }

        public static Quaternion WithEulerZ(this Quaternion q, float z)
        {
            var e = q.eulerAngles;
            e.z = z;
            return Quaternion.Euler(e);
        }
    }
}