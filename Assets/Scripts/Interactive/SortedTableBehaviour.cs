using UnityEngine;

namespace SortingAlgorithms
{
    public sealed class SortedTableBehaviour : MonoBehaviour
    {
        private Material material;

        [SerializeField]
        private SortedMaterialSharedData materialSharedData;

        [SerializeField]
        private bool isSorted;

        public bool IsSorted
        {
            get => isSorted;
            set 
            {
                isSorted = value;

                if (materialSharedData == null)
                {
                    return;
                }
                Color lightColor = isSorted ?
                    materialSharedData.SortedLightColor : materialSharedData.UnsortedLightColor;
                material.SetColor(materialSharedData.LightColorPropertyId, lightColor);
            }
        }

        private void OnValidate()
        {
            if (material != null)
            {
                IsSorted = isSorted;
            }
        }

        private void OnEnable()
        {
            if (material == null)
            {
                material = GetComponent<MeshRenderer>().material;
            }
            IsSorted = isSorted;
        }

        public void Setup(SortedMaterialSharedData materialSharedData, MeshRenderer renderer)
        {
            this.materialSharedData = materialSharedData;
            material = renderer.material;
        }
    }
}
