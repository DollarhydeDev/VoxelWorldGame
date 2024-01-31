using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Vector3 chunkSize;
    private Vector3 worldPosition;
    private float cubeSize;
    private Material material;

    public void BuildChunk(Vector3 worldPosition, Vector3 chunkSize, float cubeSize, Material material)
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer.material = material;


        this.worldPosition = worldPosition;
        this.chunkSize = chunkSize;
        this.cubeSize = cubeSize;
        this.material = material;

        meshFilter.mesh = MeshGenerator.BuildChunkMesh(worldPosition, chunkSize, cubeSize);
    }
}
