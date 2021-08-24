using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace SortingAlgorithms
{
    public sealed class SortingManager : MonoBehaviour
    {
        private SortedCubeBehaviour[] sortedCubes;
        private SortedTableBehaviour sortedTable;
        private ISorter<SortedCubeBehaviour> sorter;
        private float currentStepTime;
        private float currentDelayTime;
        private float startTime;

        [SerializeField]
        private SortedCubeRandomGenerator sortedCubeGenerator;

        [SerializeField]
        private SortedTableGenerator sortedTableGenerator;

        [SerializeField]
        private SingleSoundManager sortedItemSound;

        [SerializeField]
        private SortingAlgorithmType algorithmType;

        [SerializeField]
        [Min(0F)]
        private float timePerStep = 1F;

        [SerializeField]
        [Min(0F)]
        private float delayBeforeStart = 3F;

        private bool CanSort => sorter != null && !sorter.IsSorted ||
            currentDelayTime < delayBeforeStart;

        public bool IsSorted => sorter.IsSorted;

        public SortingAlgorithmType AlgorithmType
        {
            get => algorithmType;
            set => algorithmType = value;
        }

        public int RandomSeed
        {
            get => sortedCubeGenerator.RandomSeed;
            set => sortedCubeGenerator.RandomSeed = value;
        }

        /// <summary>
        /// Count of sorted cubes.<br/>
        /// Setter Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value cannot be less than zero.
        /// </summary>
        public int Count
        {
            get => sortedCubeGenerator.Count;
            set => sortedCubeGenerator.Count = value;
        }

        /// <summary>
        /// Time in seconds required to perform one sorting step.<br/>
        /// Setter Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value cannot be less than zero.
        /// </summary>
        public float TimePerStep
        {
            get => timePerStep;
            set
            {
                if (value < 0F)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Value cannot be less than zero.");
                }
                timePerStep = value;
            }
        }

        /// <summary>
        /// Delay in seconds before the sorting start.<br/>
        /// Setter Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Value cannot be less than zero.
        /// </summary>
        public float DelayBeforeStart
        {
            get => delayBeforeStart;
            set
            {
                if (value < 0F)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Value cannot be less than zero.");
                }
                delayBeforeStart = value;
            }
        }

        public int ComparisonCount => sorter.ComparisonCount;

        public float SortingTime
        {
            get
            {
                var sortingTime = Time.time - startTime;
                return sortingTime < 0F ? 0F : sortingTime;
            }
        }

        public ReadOnlyArray<SortedCubeBehaviour> SortedCubes => sortedCubes;

        public event Action<float> SortingTimeUpdated;

        public event Action<int> ComparisonCountUpdated;

        public event Action SequenceSorted
        {
            add => sorter.SequenceSorted += value;
            remove => sorter.SequenceSorted -= value;
        }

        public event Action<ItemIndexPair<SortedCubeBehaviour>> ItemSorted
        {
            add => sorter.ItemSorted += value;
            remove => sorter.ItemSorted -= value;
        }

        public event Action<ItemIndexPair<SortedCubeBehaviour>> ItemMovedToBackground
        {
            add => sorter.ItemMovedToBackground += value;
            remove => sorter.ItemMovedToBackground -= value;
        }

        public event Action<ItemIndexPair<SortedCubeBehaviour>> ItemMovedToForeground
        {
            add => sorter.ItemMovedToForeground += value;
            remove => sorter.ItemMovedToForeground -= value;
        }

        public event Action<
            ItemIndexPair<SortedCubeBehaviour>, ItemIndexPair<SortedCubeBehaviour>> ItemsCompared
        {
            add => sorter.ItemsCompared += value;
            remove => sorter.ItemsCompared -= value;
        }

        public void SetSortedCubes(SortedCubeBehaviour[] sortedCubes) => this.sortedCubes = sortedCubes;

        public void GenerateSortingItems()
        {
            sortedCubes = sortedCubeGenerator.Generate();
            sortedTable = sortedTableGenerator.Generate();
        }

        public void StartSorting()
        {
            sorter = SorterUtils.Create(
                algorithmType, sortedCubes, new SortedCubeBehaviourComparer());
            sorter.ItemMovedToBackground += OnCubeMovedToBackground;
            sorter.ItemMovedToForeground += OnCubeMovedToForeground;
            sorter.ItemsCompared += OnCubesComparison;
            sorter.ItemSorted += OnItemSorted;
            sorter.SequenceSorted += OnSequenceSorted;
            currentDelayTime = 0F;
            currentStepTime = 0F;
            startTime = Time.time + delayBeforeStart;
        }

        private void Awake()
        {
            sortedItemSound.Setup();
        }

        private void OnValidate()
        {
            sortedItemSound.Validate();
        }

        private void Update()
        {
            if (!CanSort)
            {
                return;
            }
            currentDelayTime += Time.deltaTime;

            if (currentDelayTime < delayBeforeStart)
            {
                return;
            }
            currentStepTime += Time.deltaTime;
            SortingTimeUpdated?.Invoke(SortingTime);

            if (currentStepTime >= timePerStep)
            {
                currentStepTime = 0F;
                sorter.NextStep();
                ComparisonCountUpdated?.Invoke(sorter.ComparisonCount);
            }
        }

        private void OnCubeMovedToForeground(ItemIndexPair<SortedCubeBehaviour> cube)
        {
            cube.Item.transform.position = sortedCubeGenerator.GetForegroundCubePosition(cube.Index);
        }

        private void OnCubeMovedToBackground(ItemIndexPair<SortedCubeBehaviour> cube)
        {
            cube.Item.transform.position = sortedCubeGenerator.GetBackgroundCubePosition(cube.Index);
        }

        private void OnCubesComparison(
            ItemIndexPair<SortedCubeBehaviour> cube1, ItemIndexPair<SortedCubeBehaviour> cube2)
        {
            //Debug.Log($"{cube1.Item.Value} ? {cube2.Item.Value}");
        }

        private void OnSequenceSorted()
        {
            sortedTable.IsSorted = true;
        }

        private void OnItemSorted(ItemIndexPair<SortedCubeBehaviour> sortedCube)
        {
            sortedCube.Item.IsSorted = true;
            sortedItemSound.Play();
        }
    }
}