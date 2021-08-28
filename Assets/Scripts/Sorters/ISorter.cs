using System;

namespace SortingAlgorithms
{
    public interface ISorter<T>
    {
        bool IsSorted { get; }

        int ComparisonCount { get; }

        public event Action<ItemIndexPair<T>> ItemMovedToForeground;

        public event Action<ItemIndexPair<T>> ItemMovedToBackground;

        public event Action<ItemIndexPair<T>, ItemIndexPair<T>> ItemsCompared;

        public event Action<ItemIndexPair<T>> ItemSorted;

        public event Action SequenceSorted;

        public void NextStep();
    }
}
