using UnityEngine;
using UnityEngine.AI;

namespace SortingAlgorithms
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RobotAnimator : MonoBehaviour
    {
        private NavMeshAgent robotAgent;

        [SerializeField]
        private Vector3 destination;

        [SerializeField]
        private Vector3 offsetFromTable;

        [SerializeField]
        private Animator animator;

        private int pickupAnimStateHash;
        private int idleAnimStateHash;
        private int walkAnimStateHash;

        public Vector3 Destination
        {
            get => destination;
            set
            {
                destination = value;
                robotAgent.SetDestination(destination);
                animator.Play(walkAnimStateHash);
            }
        }

        public Vector3 OffsetFromTable => offsetFromTable;

        private void Awake()
        {
            pickupAnimStateHash = Animator.StringToHash("Anim_PickUp");
            idleAnimStateHash = Animator.StringToHash("Idle");
            walkAnimStateHash = Animator.StringToHash("Anim_Walk");
        }

        private void Start()
        {
            robotAgent = GetComponent<NavMeshAgent>();
        }

        private void OnValidate()
        {
            if (Application.isPlaying && robotAgent != null)
            {
                robotAgent.SetDestination(destination);
            }
        }

        public void PickUp(SortedCubeBehaviour cube)
        {
            animator.Play(pickupAnimStateHash);
        }
    }
}