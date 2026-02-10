using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Transform helpers.
    /// </summary>
    public static class TransformExtensions
    {
        public static void ResetLocal(this Transform t)
        {
            if (t == null) return;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void SetX(this Transform t, float x)
        {
            if (t == null) return;
            var p = t.position;
            p.x = x;
            t.position = p;
        }

        public static void SetY(this Transform t, float y)
        {
            if (t == null) return;
            var p = t.position;
            p.y = y;
            t.position = p;
        }

        public static void SetZ(this Transform t, float z)
        {
            if (t == null) return;
            var p = t.position;
            p.z = z;
            t.position = p;
        }

        public static void DestroyChildren(this Transform t)
        {
            if (t == null) return;

            for (int i = t.childCount - 1; i >= 0; i--)
            {
                var child = t.GetChild(i);
                if (child != null) Object.Destroy(child.gameObject);
            }
        }
    }
}