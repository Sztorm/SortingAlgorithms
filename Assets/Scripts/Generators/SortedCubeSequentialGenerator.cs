using System;
using UnityEngine;

namespace SortingAlgorithms
{
    public sealed class SortedCubeSequentialGenerator : SortedCubeGenerator
    {
        [SerializeField]
        private int firstNumber;

        public int Step;

        /// <summary>
        /// Getter returns first number of the sequence.<br/>
        /// Setter sets first number of the sequence.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: First number must be in range 
        /// [<see cref="SortedCubeGenerator.MinNumber"/>, <see cref="SortedCubeGenerator.MaxNumber"/>]
        /// </summary>
        public int FirstNumber
        {
            get => firstNumber;
            set
            {
                if (value < minNumber || value > maxNumber)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "First number must be in range MinNumber..MaxNumber");
                }
                firstNumber = value;
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            firstNumber = Mathf.Clamp(firstNumber, minNumber, maxNumber);
        }

        /// <summary>
        /// Generates cubes with sequential numbers and returns an array of them.
        /// </summary>
        /// <returns></returns>
        public override SortedCubeBehaviour[] Generate()
        {
            SortedCubeBehaviour[] result = base.Generate();
            int number = firstNumber;
            int range = maxNumber - minNumber;
            int nextStep = Step % range;

            for (int i = 0; i < Count; i++)
            {
                result[i].GenerateDigits(number);
                number = Mathf.Clamp(number + nextStep, minNumber, maxNumber);        
            }
            return result;
        }
    }
}