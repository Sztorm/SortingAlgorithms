namespace SortingAlgorithms
{
    public readonly struct ItemIndexPair<T>
    {
        public readonly T Item;
        public readonly int Index;

        public ItemIndexPair(T item, int index)
        {
            Item = item;
            Index = index;
        }

        public ItemIndexPair<T> WithIndex(int index) => new ItemIndexPair<T>(Item, index);

        public ItemIndexPair<T> WithItem(T item) => new ItemIndexPair<T>(item, Index);

        public static implicit operator ItemIndexPair<T>((T Item, int Index) itemAndIndex)
            => new ItemIndexPair<T>(itemAndIndex.Item, itemAndIndex.Index);
    }
}
