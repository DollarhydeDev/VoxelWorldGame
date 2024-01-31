using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperChunk : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private List<GameObject> chunks;

    [Header("SuperChunk Settings")]
    [SerializeField] private Vector3 superChunkSize;

    [Header("Chunk Settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Vector3 chunkSize;
    [SerializeField] private float cubeSize;
    [SerializeField] private Material material;

    private void Start()
    {
        BuildSuperChunk();
    }

    private void BuildSuperChunk()
    {
        for (int x = 0; x < superChunkSize.x; x++)
        {
            for (int y = 0; y < superChunkSize.y; y++)
            {
                for (int z = 0; z < superChunkSize.z; z++)
                {
                    Vector3 chunkPosition = transform.position + (new Vector3(x * chunkSize.x, y * chunkSize.y, z * chunkSize.z) * cubeSize) / 2;

                    GameObject chunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity);
                    chunk.transform.parent = transform;

                    chunk.GetComponent<Chunk>().BuildChunk(chunkPosition, chunkSize, cubeSize, material);
                    chunks.Add(chunk);
                }
            }
        }
    }
}
