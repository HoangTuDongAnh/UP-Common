using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Renderer helpers.
    /// </summary>
    public static class RendererExtensions
    {
        public static void SetAlpha(this Renderer r, float alpha)
        {
            if (r == null) return;

            // Common case: material has _Color
            var mat = r.material;
            if (mat == null || !mat.HasProperty("_Color")) return;

            var c = mat.color;
            c.a = Mathf.Clamp01(alpha);
            mat.color = c;
        }

        public static Bounds GetBoundsSafe(this Renderer r)
        {
            return r != null ? r.bounds : new Bounds(Vector3.zero, Vector3.zero);
        }
    }
}