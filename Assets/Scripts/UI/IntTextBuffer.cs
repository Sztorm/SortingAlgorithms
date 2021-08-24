using System;
using UnityEngine.InputSystem.Utilities;

namespace SortingAlgorithms.UI
{
    public struct IntTextBuffer
    {
        private readonly char[] buffer;
        private int length;

        public int Length => length;

        public int Capacity => buffer.Length;

        public ReadOnlyArray<char> Buffer => buffer;

        /// <summary>
        /// Use it only for read-only purposes where the <see cref="Char"/>[] type is required.
        /// </summary>
        public char[] RawBuffer => buffer;

        public ReadOnlyArray<char> Text => new ReadOnlyArray<char>(buffer, 0, length);

        public IntTextBuffer(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(capacity), "Capacity cannot be less than zero.");
            }
            buffer = new char[capacity];
            length = 0;
        }

        public static int GetIntTextLength(int value)
            => GetIntTextLength(absValue: Math.Abs(value), signLength: value < 0 ? 1 : 0);

        private static int GetIntTextLength(int absValue, int signLength)
            => absValue == 0 ? 1 : (int)Math.Floor(Math.Log10(absValue)) + 1 + signLength;

        private void ClearBuffer() => Array.Clear(buffer, 0, Capacity);

        public void SetValue(int value)
        {
            int signLength = value < 0 ? 1 : 0;
            int absValue = Math.Abs(value);
            int length = GetIntTextLength(absValue, signLength);

            if (length > Capacity)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Value cannot exceed buffer's capacity.");
            }
            ClearBuffer();

            this.length = length;
            buffer[0] = '-';

            for (int i = length - 1; i >= signLength; i--)
            {
                buffer[i] = (char)('0' + (absValue % 10));
                absValue /= 10;
            }
        }
    }
}
