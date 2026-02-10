namespace HoangTuDongAnh.UP.Common.Extensions.Math
{
    /// <summary>
    /// Int helpers.
    /// </summary>
    public static class IntExtensions
    {
        public static bool InRange(this int v, int minInclusive, int maxExclusive)
            => v >= minInclusive && v < maxExclusive;

        public static int Clamp(this int v, int min, int max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }
    }
}