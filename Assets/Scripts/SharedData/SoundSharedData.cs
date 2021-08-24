using UnityEngine;

namespace SortingAlgorithms
{
    [CreateAssetMenu(
    fileName = "NewSound_" + nameof(SoundSharedData),
    menuName = nameof(SortingAlgorithms) + "/" + nameof(SoundSharedData))]
    public sealed class SoundSharedData : ScriptableObject
    {
        [SerializeField]
        private AudioClip sound;

        [SerializeField]
        [Range(min: 0F, max: 1F)]
        private float volume = 1F;

        [SerializeField]
        [Min(0F)]
        private float pitch = 1F;

        public AudioClip Sound => sound;

        public float Volume => volume;

        public float Pitch => pitch;
    }
}
