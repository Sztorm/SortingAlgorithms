using System;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public sealed class QuickSortSorter<T, TComparer> : ISorter<T>
        where TComparer : IComparer<T> 
    {
        private readonly T[] items;
        private readonly TComparer comparer;
        private bool isSorted;
        private int comparisonCount;
        private int currentStep;
        private int i;
        private int start;
        private int end;
        private T partitionP;
        private T partitionTemp;
        private int partitionI;
        private int partitionJ;
        private int partitionStart;
        private int partitionEnd;

        public bool IsSorted => isSorted;

        public int ComparisonCount => comparisonCount;

        public event Action<ItemIndexPair<T>> ItemMovedToForeground;

        public event Action<ItemIndexPair<T>> ItemMovedToBackground;

        public event Action<ItemIndexPair<T>, ItemIndexPair<T>> ItemsCompared;

        public event Action<ItemIndexPair<T>> ItemSorted;

        public event Action SequenceSorted;

        public QuickSortSorter(T[] items, TComparer comparer)
        {
            this.items = items;
            this.comparer = comparer;
            start = 0;
            end = items.Length - 1;
        }

        public void NextStep()
        {
            if (isSorted)
            {
                return;
            }
            isSorted = true;

            switch (currentStep)
            {
                case 0:
                    return;
            }
        }

        private void QuickSort()
        {
            comparisonCount++;

            if (start < end)
            {
                partitionStart = start;
                partitionEnd = end;
                Partition();

                end = i - 1;
                QuickSort();
                start = i + 1;
                QuickSort();
            }
        }

        private void Partition()
        {
            partitionP = items[partitionEnd];
            ItemMovedToForeground?.Invoke((partitionP, partitionEnd));
            comparisonCount++;

            for (partitionJ = partitionStart; partitionJ <= partitionEnd - 1; partitionJ++)
            {
                comparisonCount++;

                if (comparer.Compare(items[partitionJ], partitionP) <= 0)
                {
                    partitionI++;
                    partitionTemp = items[partitionI];
                    ItemMovedToForeground?.Invoke((partitionTemp, partitionI));
                    items[partitionI] = items[partitionJ];
                    ItemMovedToForeground?.Invoke((items[partitionI], partitionJ));
                    items[partitionJ] = partitionTemp;
                    ItemMovedToForeground?.Invoke((items[partitionJ], partitionI));
                }
            }
            partitionTemp = items[partitionI + 1];
            ItemMovedToForeground?.Invoke((partitionTemp, partitionI + 1));
            items[partitionI + 1] = items[partitionEnd];
            ItemMovedToForeground?.Invoke((items[partitionI + 1], partitionEnd));
            items[partitionEnd] = partitionTemp;
            ItemMovedToForeground?.Invoke((items[partitionEnd], partitionI + 1));
            i = partitionI + 1;
        }

        // Algorithm reference
        /*
        private void QuickSort(int[] array, int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(array, start, end);
         
                QuickSort(array, start, i - 1);
                QuickSort(array, i + 1, end);
            }
        }
         
        private int Partition(int[] array, int start, int end)
        {
            int temp;
            int p = array[end];
            int i = start - 1;
         
            for (int j = start; j <= end - 1; j++)
            {
                if (array[j] <= p)
                {
                    i++;
                    temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
         
            temp = array[i + 1];
            array[i + 1] = array[end];
            array[end] = temp;
            return i + 1;
        }
        */
    }
}
