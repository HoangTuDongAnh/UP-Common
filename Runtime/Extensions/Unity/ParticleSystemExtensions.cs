using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// ParticleSystem helpers.
    /// </summary>
    public static class ParticleSystemExtensions
    {
        /// <summary>
        /// Play particle system safely.
        /// </summary>
        public static void PlaySafe(this ParticleSystem ps, bool withChildren = true)
        {
            if (ps == null) return;
            ps.Play(withChildren);
        }

        /// <summary>
        /// Stop particle system safely.
        /// </summary>
        public static void StopSafe(this ParticleSystem ps, bool withChildren = true)
        {
            if (ps == null) return;
            ps.Stop(withChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        /// <summary>
        /// Clear particle system safely.
        /// </summary>
        public static void ClearSafe(this ParticleSystem ps, bool withChildren = true)
        {
            if (ps == null) return;
            ps.Clear(withChildren);
        }

        /// <summary>
        /// Check if particle system is currently playing.
        /// </summary>
        public static bool IsPlayingSafe(this ParticleSystem ps)
        {
            if (ps == null) return false;
            return ps.isPlaying;
        }
    }
}