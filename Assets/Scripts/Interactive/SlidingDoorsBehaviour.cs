using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SortingAlgorithms
{
    public sealed class SlidingDoorsBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Transform doorLeft;

        [SerializeField]
        private Transform doorRight;

        [SerializeField]
        private TranslationAnimator doorLeftAnimation;

        [SerializeField]
        private TranslationAnimator doorRightAnimation;

        [SerializeField]
        private SingleSoundManager doorLeftSound;

        [SerializeField]
        private SingleSoundManager doorRightSound;

        private void OnValidate()
        {
            doorLeftAnimation?.Validate();
            doorRightAnimation?.Validate();
            doorLeftSound?.Validate();
            doorRightSound?.Validate();
        }

        private void Awake()
        {
            doorLeftAnimation.Setup(doorLeft);
            doorRightAnimation.Setup(doorRight);
            doorLeftSound.Setup();
            doorRightSound.Setup();
        }

        private void Update()
        {
            doorLeftAnimation.Update();
            doorRightAnimation.Update();
        }

        public void Open()
        {
            doorLeftAnimation.TargetProgress = 1F;
            doorLeftAnimation.ProgressDirection = ProgressDirection.Positive;
            doorRightAnimation.TargetProgress = 1F;
            doorRightAnimation.ProgressDirection = ProgressDirection.Positive;
            doorLeftSound.CurrentSoundIndex = 0;
            doorRightSound.CurrentSoundIndex = 0;
            doorLeftSound.Play();
            doorRightSound.Play();
        }

        public void Close()
        {
            doorLeftAnimation.TargetProgress = 0F;
            doorLeftAnimation.ProgressDirection = ProgressDirection.Negative;
            doorRightAnimation.TargetProgress = 0F;
            doorRightAnimation.ProgressDirection = ProgressDirection.Negative;
            doorLeftSound.CurrentSoundIndex = 1;
            doorRightSound.CurrentSoundIndex = 1;
            doorLeftSound.Play();
            doorRightSound.Play();
        }
    }
}