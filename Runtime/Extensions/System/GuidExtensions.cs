using System;

namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// Guid helpers.
    /// </summary>
    public static class GuidExtensions
    {
        public static string NewShortGuid()
        {
            // 32 chars, no hyphens.
            return Guid.NewGuid().ToString("N");
        }
    }
}