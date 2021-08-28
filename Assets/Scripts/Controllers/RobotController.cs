using UnityEngine;
using UnityEngine.AI;

namespace SortingAlgorithms
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RobotController : MonoBehaviour
    {
        private NavMeshAgent robotAgent;

        [SerializeField]
        private Vector3 destination;

        [SerializeField]
        private Vector3 offsetFromTable;

        [SerializeField]
        private float distanceToStopWalking;

        [SerializeField]
        private RobotAnimator animator;

        public Vector3 Destination => destination;

        public Vector3 OffsetFromTable => offsetFromTable;

        private bool IsReady
            => robotAgent != null && animator != null && animator.Animator != null;

        private float DistanceToStopWalkingSquared
            => distanceToStopWalking * distanceToStopWalking;

        private void Awake()
        {
            robotAgent = GetComponent<NavMeshAgent>();
            Walk(destination);
        }

        private void OnValidate()
        {
            if (Application.isPlaying && IsReady)
            {
                Walk(destination);
            }
        }

        private void Update()
        {
            if (robotAgent.GetRemainingDistanceSquared() < DistanceToStopWalkingSquared)
            {
                animator.Play(RobotAnimation.Idle);
            }
        }

        public void PickUp(SortedCubeBehaviour cube)
        {
            animator.Play(RobotAnimation.PickUp);
        }

        public void Walk(Vector3 destination)
        {
            this.destination = destination;
            robotAgent.SetDestination(destination);
            animator.Play(RobotAnimation.Walk);
        }
    }
}