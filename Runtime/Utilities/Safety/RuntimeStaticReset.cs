using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Safety
{
    /// <summary>
    /// Global reset entry point (must be non-generic).
    /// </summary>
    internal static class RuntimeStaticsReset
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetAll()
        {
            HoangTuDongAnh.UP.Common.Patterns.Singleton.SingletonRuntime.Reset();
            HoangTuDongAnh.UP.Common.Patterns.Singleton.MonoSingletonRuntime.Reset();
        }
    }
}