using System;

namespace HoangTuDongAnh.UP.Common.Utilities.Safety
{
    /// <summary>
    /// Small guard helpers for arguments.
    /// </summary>
    public static class Guard
    {
        public static void NotNull(object value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
        }

        public static void NotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", name);
        }
    }
}