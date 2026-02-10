using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// LayerMask helpers.
    /// </summary>
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
            => (mask.value & (1 << layer)) != 0;

        public static LayerMask AddLayer(this LayerMask mask, int layer)
        {
            mask.value |= 1 << layer;
            return mask;
        }

        public static LayerMask RemoveLayer(this LayerMask mask, int layer)
        {
            mask.value &= ~(1 << layer);
            return mask;
        }
    }
}