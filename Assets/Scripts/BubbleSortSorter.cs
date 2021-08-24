using System;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public sealed class BubbleSortSorter<T, TComparer> : ISorter<T>
        where TComparer : IComparer<T> 
    {
        private readonly T[] items;
        private readonly TComparer comparer;
        private bool isSorted;
        private int comparisonCount;
        private int i;
        private int j;
        private int currentStep;

        public bool IsSorted => isSorted;

        public int ComparisonCount => comparisonCount;

        public event Action<ItemIndexPair<T>> ItemMovedToForeground;

        public event Action<ItemIndexPair<T>> ItemMovedToBackground;

        public event Action<ItemIndexPair<T>, ItemIndexPair<T>> ItemsCompared;

        public event Action<ItemIndexPair<T>> ItemSorted;

        public event Action SequenceSorted;

        public BubbleSortSorter(T[] items, TComparer comparer)
        {
            this.items = items;
            this.comparer = comparer;
        }

        public void NextStep()
        {
            if (isSorted)
            {
                return;
            }
            switch (currentStep)
            {
                case 0:
                    Step0();
                    return;
                case 1:
                    Step1();
                    return;
                case 2:
                    Step2();
                    return;
                case 3:
                    Step3();
                    return;
            }         
        }

        private void Step0()
        {
            if (i < items.Length)
            {
                currentStep = 1;
                return;
            }
            isSorted = true;
            SequenceSorted?.Invoke();
        }

        private void Step1()
        {
            if (j < items.Length - 1 - i)
            {
                currentStep = 2;
                return;
            }
            currentStep = 3;
        }

        private void Step2()
        {
            comparisonCount++;
            ItemsCompared?.Invoke((items[j], j), (items[j + 1], j + 1));

            if (comparer.Compare(items[j], items[j + 1]) > 0)
            {
                T temp = items[j + 1];
                ItemMovedToBackground?.Invoke((temp, j + 1));
                items[j + 1] = items[j];
                ItemMovedToForeground?.Invoke((items[j + 1], j + 1));
                items[j] = temp;
                ItemMovedToForeground?.Invoke((temp, j));
            }
            j++;
            currentStep = 1;
        }

        private void Step3()
        {
            ItemSorted?.Invoke((items[j], j));
            i++;
            j = 0;
            currentStep = 0;
        }

        // Algorithm reference
        /*
        private void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        */
    }
}
