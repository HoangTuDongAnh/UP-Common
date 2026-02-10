#if UNITY_2018_1_OR_NEWER
using UnityEngine;
using UnityEngine.AI;

namespace HoangTuDongAnh.UP.Common.Extensions.Unity
{
    /// <summary>
    /// NavMesh helpers.
    /// </summary>
    public static class NavMeshExtensions
    {
        /// <summary>
        /// Try sample a valid NavMesh position near a point.
        /// </summary>
        public static bool TrySamplePosition(Vector3 origin, float maxDistance, out Vector3 result, int areaMask = NavMesh.AllAreas)
        {
            result = origin;

            if (NavMesh.SamplePosition(origin, out var hit, maxDistance, areaMask))
            {
                result = hit.position;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if agent has reached destination within stopping distance.
        /// </summary>
        public static bool HasReachedDestination(this NavMeshAgent agent)
        {
            if (agent == null) return true;
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance)
                return false;

            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        /// <summary>
        /// Safely set destination (returns false if agent invalid).
        /// </summary>
        public static bool TrySetDestination(this NavMeshAgent agent, Vector3 destination)
        {
            if (agent == null || !agent.isOnNavMesh) return false;
            return agent.SetDestination(destination);
        }
    }
}
#endif