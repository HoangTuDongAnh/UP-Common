namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// String helpers.
    /// </summary>
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
        public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);

        /// <summary>
        /// Safe contains (null-safe).
        /// </summary>
        public static bool ContainsSafe(this string s, string value)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(value)) return false;
            return s.Contains(value);
        }
    }
}