using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.Services
{
    /// <summary>
    /// Base installer for registering services on startup.
    /// </summary>
    public abstract class ServiceInstaller : MonoBehaviour
    {
        protected abstract void Install();

        protected virtual void Awake()
        {
            Install();
        }
    }
}