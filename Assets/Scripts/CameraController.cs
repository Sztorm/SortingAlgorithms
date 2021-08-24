using UnityEngine;
using Cinemachine;

namespace SortingAlgorithms
{
    public sealed class CameraController : MonoBehaviour
    {
        private int menuVirtualCameraAnimStateHash;
        private int trackVirtualCameraAnimStateHash;
        private CinemachineTrackedDolly track;
        private float progress;
        private float targetProgress;

        [SerializeField]
        private CinemachineStateDrivenCamera virtualStateCamera;

        [SerializeField]
        private CinemachineVirtualCamera virtualMenuCamera;

        [SerializeField]
        private CinemachineVirtualCamera virtualTrackCamera;

        [SerializeField]
        private Animator cameraAnimator;

        [SerializeField]
        private Transform lookAtTarget;

        [SerializeField]
        private AnimationCurve movingSpeedTime = AnimationCurve
            .EaseInOut(timeStart: 0, valueStart: 0, timeEnd: 1, valueEnd: 1);

        [SerializeField]
        [Min(0.0000001F)]
        private float maxTime = 1F;

        public void OnValidate()
        {
            movingSpeedTime = MathUtils.Clamp01(movingSpeedTime);
        }

        private void Awake()
        {
            track = virtualTrackCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            menuVirtualCameraAnimStateHash = Animator.StringToHash("MenuVirtualCamera");
            trackVirtualCameraAnimStateHash = Animator.StringToHash("TrackVirtualCamera");
        }

        public void Update()
        {
            if (progress >= targetProgress)
            {
                return;
            }
            float deltaTimeScaled = Time.deltaTime / maxTime;
            float nextProgress = progress + deltaTimeScaled;
            progress = Mathf.Clamp01(nextProgress);
            float t = movingSpeedTime.Evaluate(progress);

            track.m_PathPosition = t;
        }

        public void StartMoving()
        {
            targetProgress = 1F;
            virtualTrackCamera.LookAt = lookAtTarget;
            cameraAnimator.Play(trackVirtualCameraAnimStateHash);
        }
    }
}
