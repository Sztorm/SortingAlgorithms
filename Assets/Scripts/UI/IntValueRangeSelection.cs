using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SortingAlgorithms.UI
{
    [Serializable]
    public sealed class IntValueRangeSelection
    {
        private IntTextBuffer selectedTextValueBuffer;

        [SerializeField]
        private Button selectPreviousButton;

        [SerializeField]
        private Button selectNextButton;

        [SerializeField]
        private TextMeshProUGUI selectedValueText;

        [SerializeField]
        private int min = 0;

        [SerializeField]
        private int max = 10;

        [SerializeField]
        private int currentValue;

        [SerializeField]
        private bool loopSelection;

        /// <summary>
        /// Getter return minimum value of the selection range.<br/>
        /// Setter sets the minimum value of the selection range.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Min value must be less than 
        /// <see cref="Max"/>.
        /// </summary>
        public int Min
        {
            get => min;
            set
            {
                if (value >= max)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Min value must be less than Max.");
                }
                min = value;
            }
        }

        /// <summary>
        /// Getter return maximum value of the selection range.<br/>
        /// Setter sets the maximum value of the selection range.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Max value must be greater than 
        /// <see cref="Min"/>.
        /// </summary>
        public int Max
        {
            get => max;
            set
            {
                if (value <= min)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Max value must be greater than Min.");
                }
                min = value;
            }
        }

        public Button SelectPreviousButton => selectPreviousButton;

        public Button SelectNextButton => selectNextButton;

        public TextMeshProUGUI SelectedValueText => selectedValueText;

        /// <summary>
        /// Getter return current selected value.<br/>
        /// Setter sets current selected value.<br/>
        /// Exceptions:<br/>
        /// <see cref="ArgumentOutOfRangeException"/>: Current value must be value in range 
        /// [<see cref="Min"/>, <see cref="Max"/>].
        /// </summary>
        public int CurrentValue
        {
            get => currentValue;
            set
            {
                if (value < min || value > max)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Current value must be value in range [Min, Max].");
                }
                currentValue = value;
                selectedTextValueBuffer.SetValue(currentValue);
                SelectedValueText.SetText(selectedTextValueBuffer);
            }
        }

        public bool LoopSelection
        {
            get => loopSelection;
            set => loopSelection = value;
        }

        public void Validate()
        {
            min = min >= max ? max - 1 : min;
            max = max <= min ? min + 1 : max;
            currentValue = Mathf.Clamp(currentValue, min, max);
        }

        public void Setup()
        {
            int minValueLength = IntTextBuffer.GetIntTextLength(min);
            int maxValueLength = IntTextBuffer.GetIntTextLength(max);
            int capacity = minValueLength > maxValueLength ? minValueLength : maxValueLength;
            selectedTextValueBuffer = new IntTextBuffer(capacity);
            selectedTextValueBuffer.SetValue(currentValue);
            selectedValueText.SetText(selectedTextValueBuffer);

            selectPreviousButton.onClick.AddListener(OnSelectPreviousButtonClick);
            selectNextButton.onClick.AddListener(OnSelectNextButtonClick);
        }

        private void OnSelectPreviousButtonClick()
        {
            if (loopSelection)
            {
                int prevValue = currentValue - 1;
                currentValue = prevValue < min ? max : prevValue;
            }
            else
            {
                currentValue = Mathf.Clamp(value: currentValue - 1, min, max);
            }
            selectedTextValueBuffer.SetValue(currentValue);
            selectedValueText.SetText(selectedTextValueBuffer);
        }

        private void OnSelectNextButtonClick()
        {
            if (loopSelection)
            {
                int nextValue = currentValue + 1;
                currentValue = nextValue > max ? min : nextValue;
            }
            else
            {
                currentValue = Mathf.Clamp(value: currentValue + 1, min, max);
            }
            selectedTextValueBuffer.SetValue(currentValue);
            selectedValueText.SetText(selectedTextValueBuffer);
        }
    }
}
