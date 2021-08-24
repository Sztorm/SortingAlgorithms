using System;
using UnityEngine;
using UnityEngine.UI;

namespace SortingAlgorithms.UI
{
    [Serializable]
    public sealed class MenuUIManager : IUIFragmentManager
    {
        [SerializeField]
        private GameObject uiHolder;

        [SerializeField]
        private SingleSelection sortingAlgorithmSelection;

        [SerializeField]
        private IntValueRangeSelection cubeCountSelection;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button quitButton;

        public IntValueRangeSelection CubeCountSelection => cubeCountSelection;

        public SingleSelection SortingAlgorithmSelection => sortingAlgorithmSelection;

        public void OnAwake()
        {
            sortingAlgorithmSelection.Setup();
            cubeCountSelection.Setup();
            startButton.onClick.AddListener(OnStartButtonClick);
            quitButton.onClick.AddListener(OnQuitButtonClick);
            cubeCountSelection.SelectNextButton.onClick.AddListener(OnSelectionButtonClick);
            cubeCountSelection.SelectPreviousButton.onClick.AddListener(OnSelectionButtonClick);
            sortingAlgorithmSelection.SelectNextButton.onClick.AddListener(OnSelectionButtonClick);
            sortingAlgorithmSelection.SelectPreviousButton.onClick
                .AddListener(OnSelectionButtonClick);
        }

        public void OnStart() {}

        public void OnValidate()
        {
            sortingAlgorithmSelection.Validate();
            cubeCountSelection.Validate();
        }

        public void Show() => uiHolder.SetActive(true);

        public void Hide() => uiHolder.SetActive(false);

        private void OnStartButtonClick()
        {
            GameManager gameManager = GameManager.Instance;
            UIManager uiManager = gameManager.UIManager;

            uiManager.GameUI.Show();
            Hide();
            gameManager.StartGame();
            uiManager.ConfirmImportantSound.Play();
        }

        private void OnQuitButtonClick()
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.QuitApplication();
            gameManager.UIManager.ConfirmImportantSound.Play();
        }

        private void OnSelectionButtonClick()
            => GameManager.Instance.UIManager.SelectSound.Play();
    }
}
