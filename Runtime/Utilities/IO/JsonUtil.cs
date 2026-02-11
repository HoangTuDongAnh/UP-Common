using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.IO
{
    /// <summary>
    /// Json helpers using Unity JsonUtility.
    /// </summary>
    public static class JsonUtil
    {
        public static string ToJson<T>(T data, bool pretty = false)
        {
            return JsonUtility.ToJson(data, pretty);
        }

        public static T FromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);
            return JsonUtility.FromJson<T>(json);
        }
    }
}