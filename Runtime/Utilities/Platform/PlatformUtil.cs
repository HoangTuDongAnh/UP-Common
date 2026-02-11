using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Platform
{
    /// <summary>
    /// Platform helpers.
    /// </summary>
    public static class PlatformUtil
    {
        public static bool IsMobile =>
#if UNITY_IOS || UNITY_ANDROID
            true;
#else
            false;
#endif

        public static bool IsEditor =>
#if UNITY_EDITOR
            true;
#else
            false;
#endif

        public static RuntimePlatform Platform => Application.platform;
    }
}