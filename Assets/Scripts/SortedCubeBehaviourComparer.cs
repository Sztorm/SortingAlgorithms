using System.Collections.Generic;

namespace SortingAlgorithms
{
    public readonly struct SortedCubeBehaviourComparer : IComparer<SortedCubeBehaviour>
    {
        public int Compare(SortedCubeBehaviour x, SortedCubeBehaviour y)
            => x.Value.CompareTo(y.Value);
    }
}
