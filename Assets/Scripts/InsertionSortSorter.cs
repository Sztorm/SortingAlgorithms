using System;
using System.Collections;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public sealed class InsertionSortSorter<T, TComparer> : ISorter<T>
        where TComparer : IComparer<T> 
    {
        private readonly T[] items;
        private readonly TComparer comparer;
        private bool isSorted;
        private int comparisonCount;
        private ItemIndexPair<T> key;
        private int i;
        private int j;
        private readonly BitArray backgroundedItems;
        private int currentStep;

        public bool IsSorted => isSorted;

        public int ComparisonCount => comparisonCount;

        public event Action<ItemIndexPair<T>> ItemMovedToForeground;

        public event Action<ItemIndexPair<T>> ItemMovedToBackground;

        public event Action<ItemIndexPair<T>, ItemIndexPair<T>> ItemsCompared;

        public event Action<ItemIndexPair<T>> ItemSorted;

        public event Action SequenceSorted;

        public InsertionSortSorter(T[] items, TComparer comparer)
        {
            this.items = items;
            this.comparer = comparer;
            i = 1;
            backgroundedItems = new BitArray(items.Length);
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
            comparisonCount++;

            if (i < items.Length)
            {
                currentStep = 1;
                return;
            }
            for (int i = items.Length - 1; i >= 0; i--)
            {
                var itemIndexPair = new ItemIndexPair<T>(items[i], i);

                if (backgroundedItems[i])
                {
                    ItemMovedToForeground?.Invoke(itemIndexPair);
                }
                ItemSorted?.Invoke(itemIndexPair);
            }
            isSorted = true;
            SequenceSorted?.Invoke();
        }

        private void Step1()
        {
            key = (items[i], i);
            j = i - 1;

            currentStep = 2;
        }

        private void Step2()
        {
            comparisonCount++;

            if (j >= 0)
            {
                ItemsCompared?.Invoke((items[j], j), key);
            }
            if (j >= 0 && comparer.Compare(items[j], key.Item) > 0)
            {
                ItemMovedToBackground?.Invoke((items[j + 1], j + 1));
                backgroundedItems[j] = true;
                items[j + 1] = items[j];
                ItemMovedToForeground?.Invoke((items[j], j + 1));
                j--;

                currentStep = 2;
                return;
            }
            currentStep = 3;
        }

        private void Step3()
        {
            ItemMovedToBackground?.Invoke((items[j + 1], j + 1));
            backgroundedItems[key.Index] = true;
            items[j + 1] = key.Item;
            ItemMovedToForeground?.Invoke((key.Item, j + 1));
            i++;
            currentStep = 0;
        }

        // Algorithm reference
        /*
        void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; ++i) 
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key) 
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }
        */
    }
}
