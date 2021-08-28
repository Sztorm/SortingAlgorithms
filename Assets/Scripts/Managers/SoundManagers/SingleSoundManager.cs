using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace SortingAlgorithms
{
    [Serializable]
    public sealed class SingleSoundManager
    {
        [SerializeField]
        private SoundSharedData[] sounds;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private int currentSoundIndex;

        public bool IsReady => audioSource != null;

        /// <summary>
        /// Getter returns current <see cref="Sounds"/> array index.<br/>
        /// Setter sets current <see cref="Sounds"/> array index.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value must be a valid index if 
        /// <see cref="Sounds"/> array is not empty; Zero otherwise.
        /// </summary>
        public int CurrentSoundIndex
        {
            get => currentSoundIndex;
            set
            {
                if (value < 0 || value > MaxSoundIndex)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Value must be a valid index if Sounds array is not " +
                        "empty; Zero otherwise.");
                }
                currentSoundIndex = value;
                Setup();
            }
        }

        public SoundSharedData CurrentSound => (sounds == null || sounds.Length == 0) ?
            null : sounds[currentSoundIndex];

        public ReadOnlyArray<SoundSharedData> Sounds => sounds;

        private int MaxSoundIndex
        {
            get
            {
                if (sounds == null)
                {
                    return 0;
                }
                int soundLength = sounds.Length;

                if (soundLength == 0)
                {
                    return 0;
                }
                return soundLength - 1;
            }
        }

        public void Validate()
        {
            currentSoundIndex = Mathf.Clamp(currentSoundIndex, 0, MaxSoundIndex);
        }

        private void SetupUnchecked()
        {
            SoundSharedData currentSound = CurrentSound;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = currentSound.Volume;
            audioSource.pitch = currentSound.Pitch;
            audioSource.clip = currentSound.Sound;
        }

        public void Setup()
        {
            if (!IsReady)
            {
                return;
            }
            SetupUnchecked();
        }

        public void Play()
        {
            if (!IsReady)
            {
                return;
            }
            SetupUnchecked();
            audioSource.Play();
        }
    }
}
