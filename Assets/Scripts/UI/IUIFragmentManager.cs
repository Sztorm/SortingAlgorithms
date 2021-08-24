namespace SortingAlgorithms.UI
{
    public interface IUIFragmentManager
    {
        void OnAwake();
        void OnStart();
        void OnValidate();
        void Show();
        void Hide();
    }
}
