using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Canvas helpers.
    /// </summary>
    public static class CanvasExtensions
    {
        /// <summary>
        /// Get the camera used by the canvas (handles overlay mode).
        /// </summary>
        public static Camera GetRenderCamera(this Canvas canvas)
        {
            if (canvas == null) return null;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) return null;
            return canvas.worldCamera;
        }

        /// <summary>
        /// Convert screen position to local point in a RectTransform, using canvas camera if needed.
        /// </summary>
        public static bool ScreenPointToLocalPointInRect(this Canvas canvas, RectTransform rect, Vector2 screenPos, out Vector2 localPoint)
        {
            localPoint = default;

            if (canvas == null || rect == null) return false;

            var cam = canvas.GetRenderCamera();
            return RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, cam, out localPoint);
        }

        /// <summary>
        /// Force rebuild layout for a RectTransform.
        /// </summary>
        public static void ForceRebuildLayout(this RectTransform rt)
        {
            if (rt == null) return;
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
        }
    }
}