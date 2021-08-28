using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;
using SortingAlgorithms.UI;

namespace SortingAlgorithms
{
    public sealed class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance => instance;

        private InputActions inputActions;
        private Vector2 pointerPosition;

        [SerializeField]
        private UIManager uiManager;

        [SerializeField]
        private CameraController cameraController;

        [SerializeField]
        private SlidingDoorsBehaviour doorsBehaviour;

        [SerializeField]
        private SortingManager sortingManager;

        [SerializeField]
        private RobotController robotController;

        public Vector2 PointerPosition => pointerPosition;

        public UIManager UIManager => uiManager;

        public SortingManager SortingManager => sortingManager;

        public RobotController RobotController => robotController;

        public event Action GamePaused;

        private void Awake()
        {
            instance = this;
            inputActions = new InputActions();
            inputActions.UI.Disable();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.UI.Pause.performed += OnUIPauseActionPerform;
            inputActions.Game.Look.performed += OnGameLookActionPerform;
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void OnUIPauseActionPerform(InputAction.CallbackContext ctx)
        {
            PauseGame();
            GamePaused?.Invoke();
        }

        private void OnGameLookActionPerform(InputAction.CallbackContext ctx)
        {
            pointerPosition = ctx.ReadValue<Vector2>();
        }

        public void QuitApplication()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }

        public void RestartGame()
        {
            inputActions.UI.Disable();
            SceneManager.LoadSceneAsync(sceneBuildIndex: 0, LoadSceneMode.Single);
        }

        public void StartGame()
        {
            inputActions.UI.Enable();
            MenuUIManager menuUI = UIManager.MenuUI;
            sortingManager.Count = menuUI.CubeCountSelection.CurrentValue;
            sortingManager.RandomSeed = Time.frameCount;
            sortingManager.AlgorithmType = (SortingAlgorithmType)menuUI
                .SortingAlgorithmSelection.CurrentValueIndex;
            sortingManager.GenerateSortingItems();
            doorsBehaviour.Open();
            cameraController.StartMoving();
            sortingManager.StartSorting();
            sortingManager.ItemMovedToBackground += OnItemMovedToBackground;
            sortingManager.ItemMovedToForeground += OnItemMovedToForeground;
            sortingManager.ItemsCompared += OnItemsCompared;
        }
        private void OnItemsCompared(
            ItemIndexPair<SortedCubeBehaviour> cube1, ItemIndexPair<SortedCubeBehaviour> cube2)
        {
            Vector3 destination = robotController.OffsetFromTable + Vector3.LerpUnclamped(
                cube1.Item.transform.position, cube2.Item.transform.position, 0.5F);

            robotController.Walk(destination);
        }

        private void OnItemMovedToForeground(ItemIndexPair<SortedCubeBehaviour> cube)
        {
            Vector3 destination = robotController.OffsetFromTable + cube.Item.transform.position;
            robotController.Walk(destination);
        }

        private void OnItemMovedToBackground(ItemIndexPair<SortedCubeBehaviour> cube)
        {
            Vector3 destination = robotController.OffsetFromTable + cube.Item.transform.position;
            robotController.Walk(destination);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
