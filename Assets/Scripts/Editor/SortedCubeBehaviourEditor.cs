using UnityEngine;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace SortingAlgorithms.Editor
{
    [CustomEditor(typeof(SortedCubeBehaviour)), CanEditMultipleObjects]
    public class SortedCubeBehaviourEditor : UEditor
    {
        private GUIContent generateDigitsButtonContent = new GUIContent(
            text: "Generate Digits", tooltip: "Available only in play mode.");
        private GUIContent clearChildrenButtonContent = new GUIContent(
            text: "Clear Children", tooltip: "Available only in play mode.");

        private void DrawPlayModeInspector()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(generateDigitsButtonContent))
            {
                SortedCubeBehaviour targetUnboxed = (SortedCubeBehaviour)target;
                targetUnboxed.GenerateDigits(targetUnboxed.Value);
            }
            if (GUILayout.Button(clearChildrenButtonContent))
            {
                SortedCubeBehaviour targetUnboxed = (SortedCubeBehaviour)target;
                targetUnboxed.ClearChildren();
            }
        }

        private void DrawEditModeInspector()
        {
            DrawDefaultInspector();

            bool prevGuiEnabled = GUI.enabled;
            GUI.enabled = false;

            GUILayout.Button(generateDigitsButtonContent);
            GUILayout.Button(clearChildrenButtonContent);

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
