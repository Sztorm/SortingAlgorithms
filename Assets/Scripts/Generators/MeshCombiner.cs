using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SortingAlgorithms
{
    public sealed class MeshCombiner : MonoBehaviour
    {
        public MeshRenderer[] MeshRenderers;

        public string CombinedMeshName = "CombinedMesh";

        public Vector3 CombinedMeshPivotPosition = new Vector3();

        public Vector3 CombinedMeshPivotRotation = new Vector3();

        private void SetTransformation(Transform transform)
        {
            transform.localScale = new Vector3(x: 1F, y: 1F, z: 1F);
            transform.localRotation = Quaternion.Euler(CombinedMeshPivotRotation);
            transform.localPosition = CombinedMeshPivotPosition;
        }

        private static void ResetTransformation(Transform transform)
        {
            transform.localScale = new Vector3(x: 1F, y: 1F, z: 1F);
            transform.localRotation = Quaternion.identity;
            transform.localPosition = new Vector3();
        }

        private CombineInstance[] CreateCombineInstances()
        {
            int meshCount = MeshRenderers.Length;
            var result = new CombineInstance[meshCount];

            for (int i = 0; i < meshCount; i++)
            {
                MeshRenderer meshRenderer = MeshRenderers[i];
                var combineInstance = new CombineInstance()
                {
                    mesh = meshRenderer.gameObject.GetComponent<MeshFilter>().sharedMesh,
                    transform = meshRenderer.transform.localToWorldMatrix,
                    lightmapScaleOffset = meshRenderer.lightmapScaleOffset,
                    realtimeLightmapScaleOffset = meshRenderer.realtimeLightmapScaleOffset
                };
                result[i] = combineInstance;
            }
            return result;
        }

        private List<Material> GetUniqueMaterials()
        {
            var result = new List<Material>(capacity: 8);
            int meshCount = MeshRenderers.Length;

            for (int i = 0; i < meshCount; i++)
            {
                Material[] sharedMaterials = MeshRenderers[i].sharedMaterials;
                int sharedMaterialsCount = sharedMaterials.Length;

                for (int j = 0; j < sharedMaterialsCount; j++)
                {
                    Material sharedMaterial = sharedMaterials[j];

                    if (result.Exists(m => m == sharedMaterial))
                    {
                        continue;
                    }
                    result.Add(sharedMaterial);
                }
            }
            return result;
        }

        /// <summary>
        /// Combines supplied meshes into one and returns <see cref="MeshFilter"/> and 
        /// <see cref="MeshRenderer"/> of the combined mesh.<br/>
        /// Exceptions:<br/>
        /// <see cref="InvalidOperationException"/>: <see cref="MeshRenderers"/> array must 
        /// contain at least one element.
        /// </summary>
        public (MeshFilter, MeshRenderer) Combine()
        {
            if (MeshRenderers == null || MeshRenderers.Length == 0)
            {
                throw new InvalidOperationException(
                    "MeshRenderers array must contain at least one element.");
            }
            Material[] uniqueSharedMaterials = GetUniqueMaterials().ToArray();
            CombineInstance[] meshesToCombine = CreateCombineInstances();
            var combinedMeshObject = new GameObject(CombinedMeshName);

            SetTransformation(combinedMeshObject.transform);

            MeshFilter meshFilter = combinedMeshObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();
            var combinedMesh = new Mesh();
            combinedMesh.indexFormat = IndexFormat.UInt32;
            combinedMesh.name = CombinedMeshName;

            combinedMesh.CombineMeshes(meshesToCombine);
            combinedMesh.Optimize();

            meshFilter.sharedMesh = combinedMesh;
            meshRenderer.sharedMaterials = uniqueSharedMaterials;

            ResetTransformation(combinedMeshObject.transform);

            return (meshFilter, meshRenderer);
        }
    }
}