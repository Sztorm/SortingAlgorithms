using UnityEngine;

namespace SortingAlgorithms
{
    public class SortedTableGenerator : MonoBehaviour
    {
        [SerializeField]
        private MeshCombiner meshCombiner;

        [SerializeField]
        private SortedCubeGenerator sortedCubeGenerator;

        [SerializeField]
        private SortedMaterialSharedData materialSharedData;

        [SerializeField]
        private Transform tableStartSegment;

        [SerializeField]
        private Transform tableMiddleSegment;

        [SerializeField]
        private Transform tableEndSegment;

        private (MeshFilter, MeshRenderer) CombineSegments(
            MeshRenderer[] segmentRenderers)
        {
            meshCombiner.CombinedMeshName = "SortedTable";
            meshCombiner.CombinedMeshPivotPosition = transform.position;
            meshCombiner.CombinedMeshPivotRotation = transform.eulerAngles;
            meshCombiner.MeshRenderers = segmentRenderers;
            return meshCombiner.Combine();
        }

        private void DestroySegments(MeshRenderer[] segmentRenderers)
        {
            for (int i = segmentRenderers.Length - 1; i >= 0; i--)
            {
                Destroy(segmentRenderers[i].gameObject);
            }
        }

        public SortedTableBehaviour Generate()
        {
            const int MinSegmentCount = 2;
            Transform parent = transform;
            Vector3 position = parent.localPosition;
            Bounds startSegmentBounds = tableStartSegment
                .GetComponent<MeshFilter>().sharedMesh.bounds;
            Bounds middleSegmentBounds = tableMiddleSegment
                .GetComponent<MeshFilter>().sharedMesh.bounds;
            Bounds endSegmentBounds = tableEndSegment
                .GetComponent<MeshFilter>().sharedMesh.bounds;
            Vector3 cubeSize = Vector3.Scale(
                sortedCubeGenerator.CubeSize, sortedCubeGenerator.CubeScale);
            Vector3 cubeSizeWithDisplacement = sortedCubeGenerator
                .DisplacementBethweenCubes + cubeSize;
            Vector3 directionNormalized = parent.TransformDirection(Vector3.forward).normalized;
            int middleSegmentCount = (int)(MathUtils.DivideComponents(
                cubeSizeWithDisplacement, middleSegmentBounds.size).magnitude *
                sortedCubeGenerator.Count) + 1;
            var segmentRenderers = new MeshRenderer[middleSegmentCount + MinSegmentCount];

            segmentRenderers[0] = Instantiate(
                tableStartSegment, position, tableStartSegment.localRotation)
                .GetComponent<MeshRenderer>();
            position += Vector3.Scale(startSegmentBounds.extents, directionNormalized);

            for (int i = 0, length = middleSegmentCount - 1; i < length; i++)
            {
                segmentRenderers[i + 1] = Instantiate(
                    tableMiddleSegment, position, tableMiddleSegment.localRotation)
                    .GetComponent<MeshRenderer>();
                position += Vector3.Scale(middleSegmentBounds.size, directionNormalized);
            }
            segmentRenderers[middleSegmentCount] = Instantiate(
                tableMiddleSegment, position, tableMiddleSegment.localRotation)
                .GetComponent<MeshRenderer>();
            position += Vector3.Scale(endSegmentBounds.extents, directionNormalized);

            segmentRenderers[middleSegmentCount + 1] = Instantiate(
                tableEndSegment, position, tableEndSegment.localRotation)
                .GetComponent<MeshRenderer>();

            (_, MeshRenderer combinedMeshRenderer) = CombineSegments(
                segmentRenderers);
            GameObject sortedTableObject = combinedMeshRenderer.gameObject;
            sortedTableObject.AddComponent<BoxCollider>();
            var sortedTable = sortedTableObject.AddComponent<SortedTableBehaviour>();
            sortedTable.Setup(materialSharedData, combinedMeshRenderer);

            DestroySegments(segmentRenderers);

            return sortedTable;
        }
    }
}