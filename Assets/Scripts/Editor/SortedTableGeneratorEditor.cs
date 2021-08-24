using UnityEngine;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace SortingAlgorithms.Editor
{
    [CustomEditor(typeof(SortedTableGenerator))]
    public class SortedTableGeneratorEditor : UEditor
    {
        protected GUIContent generateButtonContent = new GUIContent(
            text: "Generate", tooltip: "Available only in play mode.");

        protected void DrawPlayModeInspector()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(generateButtonContent))
            {
                SortedTableGenerator targetUnboxed = (SortedTableGenerator)target;
                targetUnboxed.Generate();
            }
        }

        protected void DrawEditModeInspector()
        {
            DrawDefaultInspector();

            bool prevGuiEnabled = GUI.enabled;
            GUI.enabled = false;

            GUILayout.Button(generateButtonContent);

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
