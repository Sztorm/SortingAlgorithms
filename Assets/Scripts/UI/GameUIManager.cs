using System;
using UnityEngine;

namespace SortingAlgorithms.UI
{
    [Serializable]
    public sealed class GameUIManager : IUIFragmentManager
    {
        [SerializeField]
        private GameObject uiHolder;

        [SerializeField]
        private GameMainUIManager gameMainUI;

        [SerializeField]
        private GameMenuUIManager gameMenuUI;

        public GameMainUIManager GameMainUI => gameMainUI;

        public GameMenuUIManager GameMenuUI => gameMenuUI;

        public void OnAwake()
        {
            gameMainUI.OnAwake();
            gameMenuUI.OnAwake();
            GameManager.Instance.GamePaused += OnGamePause;
        }

        public void OnStart()
        {
            gameMainUI.OnStart();
            gameMenuUI.OnStart();
        }

        public void OnValidate()
        {
            gameMainUI.OnValidate();
            gameMenuUI.OnValidate();
        }

        private void OnGamePause()
        {
            gameMainUI.Hide();
            gameMenuUI.Show();
        }

        public void Show() => uiHolder.SetActive(true);

        public void Hide() => uiHolder.SetActive(false);
    }
}
