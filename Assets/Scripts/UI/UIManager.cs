using UnityEngine;

namespace SortingAlgorithms.UI
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private SingleSoundManager confirmSound;

        [SerializeField]
        private SingleSoundManager confirmImportantSound;

        [SerializeField]
        private SingleSoundManager selectSound;

        [SerializeField]
        private MenuUIManager menuUI;

        [SerializeField]
        private GameUIManager gameUI;

        public MenuUIManager MenuUI => menuUI;

        public GameUIManager GameUI => gameUI;

        public SingleSoundManager ConfirmSound => confirmSound;

        public SingleSoundManager ConfirmImportantSound => confirmImportantSound;

        public SingleSoundManager SelectSound => selectSound;

        private void Awake()
        {
            confirmSound.Setup();
            confirmImportantSound.Setup();
            selectSound.Setup();
            gameUI.OnAwake();
            menuUI.OnAwake();
        }

        private void Start()
        {
            gameUI.OnStart();
            menuUI.OnStart();
        }

        private void OnValidate()
        {
            confirmSound.Validate();
            confirmImportantSound.Validate();
            selectSound.Validate();
            gameUI.OnValidate();
            menuUI.OnValidate();
        }
    }
}