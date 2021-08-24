using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace SortingAlgorithms
{
    public sealed class SortedCubeBehaviour : MonoBehaviour
    {
        public const int MinValue = 0;
        public const int MaxValue = 99;
        private const int DigitCount = SortedCubeSharedData.DigitCount;

        private Material material;

        [SerializeField]
        private SortedCubeSharedData sharedData;

        [SerializeField]
        [Range(MinValue, MaxValue)]
        private int value;

        [SerializeField]
        private bool isSorted;

        public bool IsSorted
        {
            get => isSorted;
            set
            {
                isSorted = value;
                SortedMaterialSharedData materialSharedData = sharedData.MaterialSharedData;
                Color lightColor = isSorted ?
                    materialSharedData.SortedLightColor : materialSharedData.UnsortedLightColor;
                material.SetColor(materialSharedData.LightColorPropertyId, lightColor);
            }
        }

        public int Value => value;

        private void OnValidate()
        {
            if (material != null)
            {
                IsSorted = isSorted;
            }
        }

        private void OnEnable()
        {
            material = GetComponent<MeshRenderer>().material;
            IsSorted = isSorted;
        }

        public void GenerateDigits(int value)
        {
            this.value = value;
            ReadOnlyArray<Transform> digitPrefabs = sharedData.DigitPrefabs;
            Quaternion rotation = Quaternion.Euler(sharedData.DigitRotation);
            int unitsIndex = value % DigitCount;
            Transform parent = transform;
            Transform unitsDigit = Instantiate(original: digitPrefabs[unitsIndex], parent);
            unitsDigit.localRotation = rotation;
            unitsDigit.GetComponent<MeshRenderer>().material = material;

            if (value >= DigitCount)
            {
                Vector3 tensDigitPosition = sharedData.DigitOffsetPosition -
                    sharedData.TwoDigitsDisplacementFromOffsetPosition;
                Vector3 unitsDigitPosition = sharedData.DigitOffsetPosition +
                    sharedData.TwoDigitsDisplacementFromOffsetPosition;
                int tensIndex = value / DigitCount;

                unitsDigit.localPosition = unitsDigitPosition;

                Transform tensDigit = Instantiate(original: digitPrefabs[tensIndex], parent);
                tensDigit.localPosition = tensDigitPosition;
                tensDigit.localRotation = rotation;
                tensDigit.GetComponent<MeshRenderer>().material = material;
            }
            else
            {
                unitsDigit.localPosition = sharedData.DigitOffsetPosition;
            }
            IsSorted = isSorted;
        }

        public void ClearChildren()
        {
            int childCount = transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}