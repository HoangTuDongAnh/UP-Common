using System.IO;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.IO
{
    /// <summary>
    /// Persistent data path helpers.
    /// </summary>
    public static class PersistentPathUtil
    {
        public static string PersistentPath => Application.persistentDataPath;

        public static string Combine(params string[] parts)
        {
            if (parts == null || parts.Length == 0) return PersistentPath;

            string path = parts[0];
            for (int i = 1; i < parts.Length; i++)
                path = Path.Combine(path, parts[i]);

            return path;
        }
    }
}