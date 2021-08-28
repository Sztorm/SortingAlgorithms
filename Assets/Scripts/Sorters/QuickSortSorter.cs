using System;
using System.Collections;
using System.Collections.Generic;

namespace SortingAlgorithms
{
    public sealed class QuickSortSorter<T, TComparer> : ISorter<T>
        where TComparer : IComparer<T>
    {
        private readonly T[] items;
        private readonly TComparer comparer;
        private readonly Stack<IEnumerator> sortStepEnumeratorStack;
        private bool isSorted;
        private int comparisonCount;

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
            sortStepEnumeratorStack = new Stack<IEnumerator>(capacity: items.Length);
            sortStepEnumeratorStack.Push(CreateQuickSortEnumerator(start: 0, end: items.Length - 1));
        }

        public void NextStep()
        {
            if (isSorted)
            {
                return;
            }
            if (sortStepEnumeratorStack.Count == 0)
            {
                isSorted = true;
                SequenceSorted?.Invoke();
                return;
            }
            sortStepEnumeratorStack.Peek().MoveNext();
        }

        private IEnumerator CreateQuickSortEnumerator(int start, int end)
        {
            if (start < end)
            {
                T temp;
                T p = items[end];
                int i = start - 1;

                for (int j = start; j <= end - 1; j++)
                {
                    comparisonCount++;
                    ItemsCompared?.Invoke((items[j], j), (p, end));

                    if (comparer.Compare(items[j], p) <= 0)
                    {
                        i++;
                        temp = items[i];
                        ItemMovedToBackground?.Invoke((temp, i));
                        yield return null;

                        items[i] = items[j];
                        ItemMovedToForeground?.Invoke((items[i], i));
                        yield return null;

                        items[j] = temp;
                        ItemMovedToForeground?.Invoke((temp, j));
                        yield return null;
                    }
                }

                temp = items[i + 1];
                ItemMovedToBackground?.Invoke((temp, i + 1));
                yield return null;

                items[i + 1] = items[end];
                ItemMovedToForeground?.Invoke((items[i + 1], i + 1));
                yield return null;

                items[end] = temp;
                ItemMovedToForeground?.Invoke((temp, end));
                yield return null;
                i++;

                sortStepEnumeratorStack.Push(CreateQuickSortEnumerator(start, i - 1));
                yield return null;

                sortStepEnumeratorStack.Push(CreateQuickSortEnumerator(i + 1, end));
                yield return null;
            }
            for (int i = start; i <= end; i++)
            {
                ItemSorted?.Invoke((items[i], i));
            }
            sortStepEnumeratorStack.Pop();
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
