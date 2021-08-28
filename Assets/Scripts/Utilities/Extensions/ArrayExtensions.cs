using System;

namespace SortingAlgorithms
{
    public static class ArrayExtensions
    {
        public static T[] WithLength<T>(this T[] source, int length)
        {
            if (length == source.Length)
            {
                return source;
            }
            int availableLength = source.Length < length ? source.Length : length;
            var result = new T[length];

            Array.Copy(
                sourceArray: source,
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 0,
                availableLength);

            return result;
        }

        public static T[] WithMinLength<T>(this T[] source, int length)
        {
            if (source.Length >= length)
            {
                return source;
            }
            var result = new T[length];

            Array.Copy(
                sourceArray: source,
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 0,
                source.Length);

            return result;
        }

        public static T[] WithMaxLength<T>(this T[] source, int length)
        {
            if (source.Length <= length)
            {
                return source;
            }
            var result = new T[length];

            Array.Copy(
                sourceArray: source,
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 0,
                length);

            return result;
        }

        public static int FindIndex<T>(this T[] source, Predicate<T> match)
            => Array.FindIndex(source, match);

        public static void Sort<T>(this T[] source) => Array.Sort(source);
    }
}
