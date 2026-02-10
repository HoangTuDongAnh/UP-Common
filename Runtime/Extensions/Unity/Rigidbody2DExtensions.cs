using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Rigidbody2D helpers.
    /// </summary>
    public static class Rigidbody2DExtensions
    {
        public static void SetVelocityX(this Rigidbody2D rb, float x)
        {
            if (rb == null) return;
            var v = rb.linearVelocity;
            v.x = x;
            rb.linearVelocity = v;
        }

        public static void SetVelocityY(this Rigidbody2D rb, float y)
        {
            if (rb == null) return;
            var v = rb.linearVelocity;
            v.y = y;
            rb.linearVelocity = v;
        }

        public static void ResetMotion(this Rigidbody2D rb)
        {
            if (rb == null) return;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        public static void AddImpulse(this Rigidbody2D rb, Vector2 impulse)
        {
            if (rb == null) return;
            rb.AddForce(impulse, ForceMode2D.Impulse);
        }
    }
}