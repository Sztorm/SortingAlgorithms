using System;
using System.Collections;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public sealed class BubbleSortSorter<T, TComparer> : ISorter<T>
        where TComparer : IComparer<T>
    {
        private readonly T[] items;
        private readonly TComparer comparer;
        private readonly IEnumerator sortStepEnumerator;
        private bool isSorted;
        private int comparisonCount;

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
            sortStepEnumerator = CreateBubbleSortEnumerator();
        }

        public void NextStep()
        {
            if (isSorted)
            {
                return;
            }
            sortStepEnumerator.MoveNext();
        }

        private IEnumerator CreateBubbleSortEnumerator()
        {
            for (int i = 0; i < items.Length; i++)
            {
                int j = 0;

                for (; j < items.Length - 1 - i; j++)
                {
                    comparisonCount++;
                    ItemsCompared?.Invoke((items[j], j), (items[j + 1], j + 1));
                    yield return null;

                    if (comparer.Compare(items[j], items[j + 1]) > 0)
                    {
                        T temp = items[j + 1];
                        ItemMovedToBackground?.Invoke((temp, j + 1));
                        yield return null;

                        items[j + 1] = items[j];
                        ItemMovedToForeground?.Invoke((items[j + 1], j + 1));
                        yield return null;

                        items[j] = temp;
                        ItemMovedToForeground?.Invoke((temp, j));
                        yield return null;
                    }
                }
                ItemSorted?.Invoke((items[j], j));
            }
            isSorted = true;
            SequenceSorted?.Invoke();
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
