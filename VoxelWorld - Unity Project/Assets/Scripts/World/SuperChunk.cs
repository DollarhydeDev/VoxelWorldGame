using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperChunk : MonoBehaviour
{
    private Vector3 superChunkSize;
    private Vector3 chunkSize;
    private float cubeSize;
    private GameObject chunkPrefab;
    private Material material;

    private float buildDelay;
    private float chunkBuildDelay;

    public void BuildSuperChunk(Vector3 superChunkSize, Vector3 chunkSize, float cubeSize, GameObject chunkPrefab, Material material, float buildDelay, float chunkBuildDelay)
    {
        // Settings passed from the World class.
        this.superChunkSize = superChunkSize;
        this.chunkSize = chunkSize;
        this.cubeSize = cubeSize;
        this.chunkPrefab = chunkPrefab;
        this.material = material;
        this.buildDelay = buildDelay;
        this.chunkBuildDelay = chunkBuildDelay;

        // Coroutine to build the super chunk.
        StartCoroutine(BuildSuperChunkRoutine());
    }

    private IEnumerator BuildSuperChunkRoutine()
    {
        for (int x = 0; x < superChunkSize.x; x++)
        {
            for (int y = 0; y < superChunkSize.y; y++)
            {
                for (int z = 0; z < superChunkSize.z; z++)
                {
                    // Calculate the position of the chunk.
                    float chunkX = x * chunkSize.x * cubeSize;
                    float chunkY = y * chunkSize.y * cubeSize;
                    float chunkZ = z * chunkSize.z * cubeSize;
                    Vector3 chunkPosition = transform.position + (new Vector3(chunkX, chunkY, chunkZ) / 2); // Divide by 2 to center the chunk.

                    // Instantiate the chunk and build it.
                    GameObject chunk = Instantiate(chunkPrefab, chunkPosition, chunkPrefab.transform.rotation, transform);
                    chunk.name = $"Chunk {chunkPosition}";
                    Chunk chunkScript = chunk.GetComponent<Chunk>();
                    chunkScript.BuildChunk(chunkSize, cubeSize, material, chunkBuildDelay);

                    // Delay the build of the next chunk.
                    yield return TimeSpan.FromSeconds(buildDelay);
                }
            }
        }
    }
}
