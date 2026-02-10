using System.Text;

namespace HoangTuDongAnh.UP.Common.Extensions.System
{
    /// <summary>
    /// StringBuilder helpers.
    /// </summary>
    public static class StringBuilderExtensions
    {
        public static void ClearFast(this StringBuilder sb)
        {
            if (sb == null) return;
            sb.Length = 0;
        }
    }
}