using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Physics helpers (3D).
    /// </summary>
    public static class PhysicsExtensions
    {
        /// <summary>
        /// Raycast and return hit point (or fallback).
        /// </summary>
        public static bool RaycastPoint(Ray ray, float maxDistance, out Vector3 point, int layerMask = Physics.DefaultRaycastLayers)
        {
            if (Physics.Raycast(ray, out var hit, maxDistance, layerMask))
            {
                point = hit.point;
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        /// <summary>
        /// Raycast from camera to screen position.
        /// </summary>
        public static bool RaycastFromScreen(Camera cam, Vector2 screenPos, float maxDistance, out RaycastHit hit, int layerMask = Physics.DefaultRaycastLayers)
        {
            hit = default;
            if (cam == null) return false;

            var ray = cam.ScreenPointToRay(screenPos);
            return Physics.Raycast(ray, out hit, maxDistance, layerMask);
        }
    }
}