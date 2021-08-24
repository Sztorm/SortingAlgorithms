using System;
using UnityEngine;
using TMPro;

namespace SortingAlgorithms.UI
{
    [Serializable]
    public sealed class GameMainUIManager : IUIFragmentManager
    {
        private IntTextBuffer comparisonsValueTextBuffer;
        private IntTextBuffer sortingTimeValueTextBuffer;

        [SerializeField]
        private GameObject uiHolder;

        [SerializeField]
        private TextMeshProUGUI comparisonsValueText;

        [SerializeField]
        private TextMeshProUGUI sortingTimeValueText;

        public void OnAwake()
        {
            comparisonsValueTextBuffer = new IntTextBuffer(capacity: 6);
            sortingTimeValueTextBuffer = new IntTextBuffer(capacity: 6);
            comparisonsValueTextBuffer.SetValue(0);
            comparisonsValueText.SetText(comparisonsValueTextBuffer);
            sortingTimeValueTextBuffer.SetValue(0);
            sortingTimeValueText.SetText(sortingTimeValueTextBuffer);
        }

        public void OnStart() 
        {
            var sortingManager = GameManager.Instance.SortingManager;
            sortingManager.ComparisonCountUpdated += OnComparisonsValueChange;
            sortingManager.SortingTimeUpdated += OnSortingTimeValueChange;
        }

        public void OnValidate() {}

        public void Show() => uiHolder.SetActive(true);

        public void Hide() => uiHolder.SetActive(false);

        private void OnComparisonsValueChange(int value)
        {
            comparisonsValueTextBuffer.SetValue(value);
            comparisonsValueText.SetText(comparisonsValueTextBuffer);
        }

        private void OnSortingTimeValueChange(float value)
        {
            sortingTimeValueTextBuffer.SetValue((int)value);
            sortingTimeValueText.SetText(sortingTimeValueTextBuffer);
        }
    }
}
