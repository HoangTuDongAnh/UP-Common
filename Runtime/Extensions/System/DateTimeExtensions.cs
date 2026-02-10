using System;

namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// DateTime helpers.
    /// </summary>
    public static class DateTimeExtensions
    {
        public static long ToUnixSeconds(this DateTime dt)
            => new DateTimeOffset(dt).ToUnixTimeSeconds();

        public static long ToUnixMilliseconds(this DateTime dt)
            => new DateTimeOffset(dt).ToUnixTimeMilliseconds();

        public static DateTime FromUnixSeconds(long seconds)
            => DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;

        public static DateTime FromUnixMilliseconds(long ms)
            => DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
    }
}