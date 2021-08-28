using UnityEngine;
using UnityEngine.AI;

namespace SortingAlgorithms
{
    public static class NavMeshAgentExtensions
    {
        public static float GetRemainingDistanceSquared(this NavMeshAgent source)
        {
            Vector3 difference = source.transform.position - source.destination;

            return difference.x * difference.x + 
                difference.y * difference.y + 
                difference.z * difference.z;
        }
    }
}
