using System.IO;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Utilities.IO
{
    /// <summary>
    /// Small file IO helpers.
    /// </summary>
    public static class FileUtilLite
    {
        public static bool TryWriteAllText(string path, string content)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(path, content);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        public static bool TryReadAllText(string path, out string content)
        {
            content = null;

            try
            {
                if (!File.Exists(path)) return false;
                content = File.ReadAllText(path);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}