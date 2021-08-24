using URandom = UnityEngine.Random;

namespace SortingAlgorithms
{
    public sealed class SortedCubeRandomGenerator : SortedCubeGenerator
    {
        public int RandomSeed;

        /// <summary>
        /// Generates cubes with random numbers and returns an array of them.
        /// </summary>
        /// <returns></returns>
        public override SortedCubeBehaviour[] Generate()
        {
            SortedCubeBehaviour[] result = base.Generate();
            URandom.InitState(RandomSeed);

            for (int i = 0; i < Count; i++)
            {
                result[i].GenerateDigits(URandom.Range(minNumber, maxNumber));   
            }
            return result;
        }
    }
}