using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTesting : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;


    [SerializeField] private Vector3 chunkSize = new Vector3(5, 5, 5);
    [SerializeField] private Vector3 worldPosition = new Vector3(0, 0, 0);
    [SerializeField] private float cubeSize = 1;
    [SerializeField] private Material material;

    private void Start()
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = material;

        Mesh chunkMesh = MeshGenerator.BuildChunk(worldPosition, chunkSize, cubeSize);

        meshFilter.mesh = chunkMesh;
    }
}
