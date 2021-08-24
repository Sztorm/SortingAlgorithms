using UnityEngine;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace SortingAlgorithms.Editor
{
    [CustomEditor(typeof(SlidingDoorsBehaviour)), CanEditMultipleObjects]
    public sealed class SlidingDoorsBehaviourEditor : UEditor
    {
        private static readonly GUIContent OpenButtonContent = new GUIContent(
            text: "Open", tooltip: "Available only in play mode.");
        private static readonly GUIContent CloseButtonContent = new GUIContent(
            text: "Close", tooltip: "Available only in play mode.");

        private void DrawPlayModeInspector()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(OpenButtonContent))
            {
                SlidingDoorsBehaviour targetUnboxed = (SlidingDoorsBehaviour)target;
                targetUnboxed.Open();
            }
            if (GUILayout.Button(CloseButtonContent))
            {
                SlidingDoorsBehaviour targetUnboxed = (SlidingDoorsBehaviour)target;
                targetUnboxed.Close();
            }
        }

        private void DrawEditModeInspector()
        {
            DrawDefaultInspector();

            bool prevGuiEnabled = GUI.enabled;
            GUI.enabled = false;

            GUILayout.Button(OpenButtonContent);
            GUILayout.Button(CloseButtonContent);

            GUI.enabled = prevGuiEnabled;
        }

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                DrawPlayModeInspector();
            }
            else
            {
                DrawEditModeInspector();
            }
        }
    }
}
