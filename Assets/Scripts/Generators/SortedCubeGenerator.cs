using System;
using UnityEngine;

namespace SortingAlgorithms
{
    public class SortedCubeGenerator : MonoBehaviour
    {
        public const int NumberMinValue = SortedCubeBehaviour.MinValue;
        public const int NumberMaxValue = SortedCubeBehaviour.MaxValue;

        [SerializeField]
        protected Transform sortedCubePrefab;

        [SerializeField]
        protected int count;

        [SerializeField]
        [Range(NumberMinValue, NumberMaxValue)]
        protected int minNumber = 0;

        [SerializeField]
        [Range(NumberMinValue, NumberMaxValue)]
        protected int maxNumber = 99;

        public Vector3 DisplacementBethweenCubes = new Vector3(x: 0.75F, y: 0F, z: 0F);

        public Vector3 CubeRotation = new Vector3(x: 0F, y: 0F, z: 0F);

        public Vector3 CubeScale = new Vector3(x: 1F, y: 1F, z: 1F);

        public Vector3 CubeSize
            => sortedCubePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size;

        public int MinNumber => minNumber;

        public int MaxNumber => maxNumber;

        /// <summary>
        /// Count of generated cubes.<br/>
        /// Setter Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value cannot be less than zero.
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Value cannot be less than zero.");
                }
                count = value;
            }
        }

        /// <summary>
        /// Validates minNumber and maxNumber range.
        /// </summary>
        protected virtual void OnValidate()
        {
            int minNumberMax = (maxNumber - 1) < 0 ? 0 : maxNumber - 1;
            minNumber = Mathf.Clamp(minNumber, NumberMinValue, minNumberMax);
            maxNumber = Mathf.Clamp(maxNumber, minNumber + 1, NumberMaxValue);
        }

        /// <summary>
        /// Sets range of numbers on cubes.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>:<br/>
        /// Minimum must be less than maximum;<br/>
        /// Minimum cannot be less than <see cref="NumberMinValue"/>;<br/>
        /// Maximum cannot be greater than <see cref="NumberMaxValue"/>.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void SetNumberRange(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(min), "Minimum must be less than maximum.");
            }
            if (min < NumberMinValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(min), $"Minimum cannot be less than {NumberMinValue} value.");
            }
            if (max > NumberMaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(max), $"Maximum cannot be greater than {NumberMaxValue} value.");
            }
            minNumber = min;
            maxNumber = max;
        }

        public Vector3 GetForegroundCubePosition(int index)
            => transform.position + DisplacementBethweenCubes * index;

        public Vector3 GetBackgroundCubePosition(int index)
            => GetForegroundCubePosition(index) + new Vector3(
                DisplacementBethweenCubes.z,
                DisplacementBethweenCubes.y,
                DisplacementBethweenCubes.x);

        /// <summary>
        /// Generates cubes and returns an array of them.
        /// </summary>
        /// <returns></returns>
        public virtual SortedCubeBehaviour[] Generate()
        {
            var result = new SortedCubeBehaviour[count];
            Transform parent = transform;
            Quaternion rotation = Quaternion.Euler(CubeRotation);
            Vector3 firstposition = parent.position;

            for (int i = 0; i < count; i++)
            {
                Vector3 position = firstposition + DisplacementBethweenCubes * i;

                Transform cubeTransform = Instantiate(sortedCubePrefab, position, rotation);
                cubeTransform.localScale = CubeScale;

                result[i] = cubeTransform.GetComponent<SortedCubeBehaviour>();
            }
            return result;
        }
    }
}