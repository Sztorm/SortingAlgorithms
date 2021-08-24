using System;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public static class SorterUtils
    {
        /// <summary>
        /// Creates a sorter depending on specified algorithm type.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentException"/>: Specified algorithm type argument do not represent 
        /// a valid enum value.
        /// </summary>
        /// <param name="algorithmType"></param>
        /// <returns></returns>
        public static ISorter<T> Create<T, TComparer>(
            SortingAlgorithmType algorithmType, T[] items, TComparer comparer)
            where TComparer : IComparer<T>
        {
            switch (algorithmType)
            {
                case SortingAlgorithmType.BubbleSort:
                    return new BubbleSortSorter<T, TComparer>(items, comparer);
                case SortingAlgorithmType.QuickSort:
                    return new QuickSortSorter<T, TComparer>(items, comparer);
                case SortingAlgorithmType.InsertionSort:
                    return new InsertionSortSorter<T, TComparer>(items, comparer);
            }
            throw new ArgumentException(
                nameof(algorithmType),
                "Specified algorithm type argument do not represent a valid enum value.");
        }
    }
}
