using UnityEngine;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace SortingAlgorithms.Editor
{
    [CustomEditor(typeof(MeshCombiner))]
    public sealed class MeshCombinerEditor : UEditor
    {
        private static readonly GUIContent CombineButtonContent = new GUIContent(
          text: "Combine", tooltip: "Combines referenced meshes into one.");

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(CombineButtonContent))
            {
                MeshCombiner targetUnboxed = (MeshCombiner)target;
                targetUnboxed.Combine();
            }
        }
    }
}