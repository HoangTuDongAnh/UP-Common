using System;

namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// Enum helpers.
    /// </summary>
    public static class EnumExtensions
    {
        public static bool HasFlagFast<TEnum>(this TEnum value, TEnum flag)
            where TEnum : struct, Enum
        {
            ulong v = Convert.ToUInt64(value);
            ulong f = Convert.ToUInt64(flag);
            return (v & f) == f;
        }
    }
}