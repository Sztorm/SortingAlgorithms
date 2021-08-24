using TMPro;

namespace SortingAlgorithms.UI
{
    public static class TMP_TextExtensions
    {
        public static void SetText(this TMP_Text source, IntTextBuffer textBuffer)
            => source.SetText(textBuffer.RawBuffer, start: 0, textBuffer.Length);
    }
}
