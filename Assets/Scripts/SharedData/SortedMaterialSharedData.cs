using UnityEngine;

namespace SortingAlgorithms
{
    [CreateAssetMenu(
        fileName = nameof(SortedMaterialSharedData),
        menuName = nameof(SortingAlgorithms) + "/" + nameof(SortedMaterialSharedData))]
    public sealed class SortedMaterialSharedData : ScriptableObject
    {
        private int lightColorPropertyId;

        [SerializeField]
        [ColorUsage(showAlpha: false, hdr: true)]
        private Color unsortedLightColor = new Color(r: 2F, g: 0.4F, b: 0.4F, a: 1F);

        [ColorUsage(showAlpha: false, hdr: true)]
        [SerializeField]
        private Color sortedLightColor = new Color(r: 0.4F, g: 2F, b: 0.4F, a: 1F);

        public int LightColorPropertyId => lightColorPropertyId;

        public Color UnsortedLightColor => unsortedLightColor;

        public Color SortedLightColor => sortedLightColor;

        private void Awake()
        {
            lightColorPropertyId = Shader.PropertyToID("_LightColor");
        }
    }
}
