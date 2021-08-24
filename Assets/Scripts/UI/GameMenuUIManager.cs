using System;
using UnityEngine;
using UnityEngine.UI;

namespace SortingAlgorithms.UI
{
    [Serializable]
    public sealed class GameMenuUIManager : IUIFragmentManager
    {
        [SerializeField]
        private GameObject uiHolder;

        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button quitButton;

        public void OnAwake()
        {
            resumeButton.onClick.AddListener(OnResumeButtonClick);
            restartButton.onClick.AddListener(OnRestartButtonClick);
            quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        public void OnStart() {}

        public void OnValidate() {}

        public void Show() => uiHolder.SetActive(true);

        public void Hide() => uiHolder.SetActive(false);

        private void OnRestartButtonClick()
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.ResumeGame();
            gameManager.RestartGame();
            gameManager.UIManager.ConfirmSound.Play();
        }

        private void OnResumeButtonClick()
        {
            Hide();
            GameManager gameManager = GameManager.Instance;
            UIManager uiManager = gameManager.UIManager;
            uiManager.GameUI.GameMainUI.Show();
            uiManager.ConfirmImportantSound.Play();
            gameManager.ResumeGame();
        }

        private void OnQuitButtonClick()
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.QuitApplication();
            gameManager.UIManager.ConfirmImportantSound.Play();
        }
    }
}
