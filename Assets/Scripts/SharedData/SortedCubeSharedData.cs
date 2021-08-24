using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace SortingAlgorithms
{
    [CreateAssetMenu(
        fileName = nameof(SortedCubeSharedData),
        menuName = nameof(SortingAlgorithms) + "/" + nameof(SortedCubeSharedData))]
    public sealed class SortedCubeSharedData : ScriptableObject
    {
        public const int DigitCount = 10;

        [SerializeField]
        private SortedMaterialSharedData materialSharedData;

        [SerializeField]
        private Transform[] digitPrefabs = new Transform[DigitCount];

        [SerializeField]
        private Vector3 digitOffsetPosition = new Vector3(x: 0F, y: 0F, z: -0.25F);

        [SerializeField]
        private Vector3 digitRotation = new Vector3(x: 0F, y: 0F, z: 0F);

        [SerializeField]
        private Vector3 twoDigitsDisplacementFromOffsetPosition = new Vector3(x: 0.1F, y: 0F, z: 0F);

        public SortedMaterialSharedData MaterialSharedData => materialSharedData;

        public ReadOnlyArray<Transform> DigitPrefabs => digitPrefabs;

        public Vector3 DigitOffsetPosition => digitOffsetPosition;

        public Vector3 DigitRotation => digitRotation;

        public Vector3 TwoDigitsDisplacementFromOffsetPosition
            => twoDigitsDisplacementFromOffsetPosition;

        private void OnValidate()
        {
            digitPrefabs = digitPrefabs.WithLength(DigitCount);
        }
    }
}
