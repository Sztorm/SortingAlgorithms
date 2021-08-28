using System;
using UnityEngine;

namespace SortingAlgorithms
{
    [Serializable]
    public sealed class RobotAnimator
    {
        private RobotAnimation currentAnimation;

        [SerializeField]
        private Animator animator;

        public Animator Animator => animator;

        public RobotAnimation CurrentAnimation => currentAnimation;

        public void Play(RobotAnimation animation)
        {
            if (currentAnimation == animation)
            {
                return;
            }
            currentAnimation = animation;
            animator.Play(animation.HashValue, layer: -1, normalizedTime: 0.25F);
        }
    }
}
