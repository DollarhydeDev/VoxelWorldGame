using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private Vector3 chunkSize = new Vector3(5, 5, 5);
    private Vector3 worldPosition = new Vector3(0, 0, 0);
    private float cubeSize = 1;
    private Material material;

    private void Start()
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = material;

        Mesh testMesh = MeshGenerator.BuildChunkMesh(worldPosition, chunkSize, cubeSize);

        meshFilter.mesh = testMesh;
    }

    public void BuildChunk(Vector3 worldPosition, Vector3 chunkSize, float cubeSize, Material material)
    {
        this.worldPosition = worldPosition;
        this.chunkSize = chunkSize;
        this.cubeSize = cubeSize;
        this.material = material;

        Mesh chunkMesh = MeshGenerator.BuildChunkMesh(worldPosition, chunkSize, cubeSize);
    }
}
