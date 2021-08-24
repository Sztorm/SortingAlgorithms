using System;
using UnityEngine;

namespace SortingAlgorithms
{
    [Serializable]
    public sealed class TranslationAnimator
    {
        private Transform transform;
        private Vector3 startPosition;
        private float progress;

        [SerializeField]
        private Vector3 translation = new Vector3(x: -2F, y: 0F, z: 0F);

        [SerializeField]
        private AnimationCurve translationSpeedTime = AnimationCurve
            .EaseInOut(timeStart: 0, valueStart: 0, timeEnd: 1, valueEnd: 1);

        [SerializeField]
        [Min(0.0000001F)]
        private float maxTime = 1F;

        [SerializeField]
        private float targetProgress;

        [SerializeField]
        public ProgressDirection ProgressDirection;

        public Vector3 EndPosition => startPosition + translation;

        public Vector3 StartPosition => startPosition;

        public Vector3 Translation => translation;

        public AnimationCurve TranslationSpeedTime => translationSpeedTime;

        /// <summary>
        /// Getter returns target progress ot the animation.<br/>
        /// Setter sets target progress ot the animation.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value must be in range [0, 1].
        /// </summary>
        public float TargetProgress
        {
            get => targetProgress;
            set
            {
                if (value < 0F || value > 1F)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Value must be in range [0, 1].");
                }
                targetProgress = value;
            }
        }

        public float Progress => progress;

        public float MaxTime => maxTime;

        public void Validate()
        {
            targetProgress = Mathf.Clamp01(targetProgress);
            translationSpeedTime = MathUtils.Clamp01(translationSpeedTime);
        }

        public void Setup(Transform transform)
        {
            this.transform = transform;
            startPosition = transform.localPosition;
        }

        public void Update()
        {
            if (ProgressDirection == ProgressDirection.None)
            {
                return;
            }
            float deltaTimeScaled = Time.deltaTime / maxTime;
            float nextProgress = progress + (deltaTimeScaled * (float)ProgressDirection);

            if (nextProgress.CompareTo(targetProgress) == (int)ProgressDirection)
            {
                return;
            } 
            progress = Mathf.Clamp01(nextProgress);
            float t = translationSpeedTime.Evaluate(progress);

            transform.localPosition = Vector3.LerpUnclamped(startPosition, EndPosition, t);
        }
    }
}
