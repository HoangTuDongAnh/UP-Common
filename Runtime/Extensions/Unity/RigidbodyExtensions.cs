using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// Rigidbody helpers.
    /// </summary>
    public static class RigidbodyExtensions
    {
        /// <summary>
        /// Set velocity X while keeping Y and Z.
        /// </summary>
        public static void SetVelocityX(this Rigidbody rb, float x)
        {
            if (rb == null) return;
            var v = rb.velocity;
            v.x = x;
            rb.velocity = v;
        }

        /// <summary>
        /// Set velocity Y while keeping X and Z.
        /// </summary>
        public static void SetVelocityY(this Rigidbody rb, float y)
        {
            if (rb == null) return;
            var v = rb.velocity;
            v.y = y;
            rb.velocity = v;
        }

        /// <summary>
        /// Clear velocity and angular velocity.
        /// </summary>
        public static void ResetMotion(this Rigidbody rb)
        {
            if (rb == null) return;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        /// <summary>
        /// Add force instantly (Impulse mode).
        /// </summary>
        public static void AddImpulse(this Rigidbody rb, Vector3 impulse)
        {
            if (rb == null) return;
            rb.AddForce(impulse, ForceMode.Impulse);
        }
    }
}