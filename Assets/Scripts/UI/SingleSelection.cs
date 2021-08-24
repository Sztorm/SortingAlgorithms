using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using TMPro;

namespace SortingAlgorithms
{
    [Serializable]
    public sealed class SingleSelection
    {
        [SerializeField]
        private Button selectPreviousButton;

        [SerializeField]
        private Button selectNextButton;

        [SerializeField]
        private string[] values; 

        [SerializeField]
        private TextMeshProUGUI selectedValueText;

        [SerializeField]
        private int currentValueIndex;

        [SerializeField]
        private bool loopSelection;

        public ReadOnlyArray<string> Values => values;

        public Button SelectPreviousButton => selectPreviousButton;

        public Button SelectNextButton => selectNextButton;

        public TextMeshProUGUI SelectedValueText => selectedValueText;

        /// <summary>
        /// Getter return current value index of the selection array.<br/>
        /// Setter sets current value index of the selection array.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Current value index must be in 
        /// <see cref="Values"/> array range.
        /// </summary>
        public int CurrentValueIndex
        {
            get => currentValueIndex;
            set
            {
                if (value < 0 || value >= values.Length)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Current value index must be in Values array range.");
                }

                currentValueIndex = value;
                SelectedValueText.SetText(values[currentValueIndex]);
            }
        }

        public bool LoopSelection
        {
            get => loopSelection;
            set => loopSelection = value;
        }

        public void Validate()
        {
            currentValueIndex = Mathf.Clamp(
                currentValueIndex, min: 0, max: values.Length - 1);
        }

        public void Setup()
        {
            SelectedValueText.SetText(values[currentValueIndex]);
            selectPreviousButton.onClick.AddListener(OnSelectPreviousButtonClick);
            selectNextButton.onClick.AddListener(OnSelectNextButtonClick);
        }

        private void OnSelectPreviousButtonClick()
        {
            if (loopSelection)
            {
                currentValueIndex = (currentValueIndex + values.Length - 1) % values.Length;
            }
            else
            {
                currentValueIndex = Mathf.Clamp(
                    value: currentValueIndex - 1, min: 0, max: values.Length - 1);
            }
            SelectedValueText.SetText(values[currentValueIndex]);
        }

        private void OnSelectNextButtonClick()
        {
            if (loopSelection)
            {
                currentValueIndex = (currentValueIndex + 1) % values.Length;
            }
            else
            {
                currentValueIndex = Mathf.Clamp(
                    value: currentValueIndex + 1, min: 0, max: values.Length - 1);
            }
            SelectedValueText.SetText(values[currentValueIndex]);
        }
    }
}
