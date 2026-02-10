using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// PlayerPrefs typed helpers.
    /// </summary>
    public static class PlayerPrefsExtensions
    {
        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public static void SetVector3(string key, Vector3 v)
        {
            PlayerPrefs.SetFloat($"{key}_x", v.x);
            PlayerPrefs.SetFloat($"{key}_y", v.y);
            PlayerPrefs.SetFloat($"{key}_z", v.z);
        }

        public static Vector3 GetVector3(string key, Vector3 defaultValue)
        {
            float x = PlayerPrefs.GetFloat($"{key}_x", defaultValue.x);
            float y = PlayerPrefs.GetFloat($"{key}_y", defaultValue.y);
            float z = PlayerPrefs.GetFloat($"{key}_z", defaultValue.z);
            return new Vector3(x, y, z);
        }
    }
}