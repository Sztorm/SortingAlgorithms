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

        public InsertionSortSorter(T[] items, TComparer comparer)
        {
            this.items = items;
            this.comparer = comparer;
            sortStepEnumerator = CreateInsertionSortEnumerator();
        }

        public void NextStep()
        {
            if (isSorted)
            {
                return;
            }
            sortStepEnumerator.MoveNext();
        }

        private IEnumerator CreateInsertionSortEnumerator()
        {
            int keyIndex = 0;

            for (int i = 1; i < items.Length; ++i)
            {
                T key = items[i];
                int j = i - 1;

                if (j >= 0 && comparer.Compare(items[j], key) > 0)
                {
                    do
                    {
                        comparisonCount++;
                        ItemsCompared?.Invoke((items[j], j), (key, i));
                        yield return null;

                        ItemMovedToBackground?.Invoke((items[j + 1], j + 1));
                        yield return null;

                        items[j + 1] = items[j];

                        ItemMovedToForeground?.Invoke((items[j + 1], j + 1));
                        yield return null;
                        j--;
                    }
                    while (j >= 0 && comparer.Compare(items[j], key) > 0);
                }
                else
                {
                    comparisonCount++;
                    ItemsCompared?.Invoke((items[j], j), (key, i));
                }
                ItemMovedToBackground?.Invoke((items[j + 1], j + 1));
                yield return null;

                items[j + 1] = key;

                ItemMovedToForeground?.Invoke((key, j + 1));
                yield return null;


                if (keyIndex != (j + 1))
                {
                    UnityEngine.Debug.Log(keyIndex);

                    for (int k = j + 1; k >= keyIndex; k--)
                    {
                        ItemMovedToForeground?.Invoke((items[k], k));
                        yield return null;
                    }
                    keyIndex = j + 1;
                }
            }
            for (int i = items.Length - 1; i >= keyIndex; i--)
            {
                ItemMovedToForeground?.Invoke((items[i], i));
                yield return null;
            }
            for (int i = 0; i < items.Length; i++)
            {
                ItemSorted?.Invoke((items[i], i));
            }
            isSorted = true;
            SequenceSorted?.Invoke();
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
