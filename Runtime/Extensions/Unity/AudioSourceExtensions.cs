using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// AudioSource helpers.
    /// </summary>
    public static class AudioSourceExtensions
    {
        public static void PlayOneShotSafe(this AudioSource src, AudioClip clip, float volumeScale = 1f)
        {
            if (src == null || clip == null) return;
            src.PlayOneShot(clip, volumeScale);
        }

        public static void StopSafe(this AudioSource src)
        {
            if (src == null) return;
            src.Stop();
        }
    }
}