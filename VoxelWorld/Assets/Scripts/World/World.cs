using System;
using System.Collections;
using UnityEngine;

public class World : MonoBehaviour
{
    [Header("World Settings")]
    [SerializeField] private Vector3 worldSize;

    [Header("SuperChunk Settings")]
    [SerializeField] private Vector3 superChunkSize;

    [Header("Chunk Settings")]
    [SerializeField] private Vector3 chunkSize;

    [Header("Cube Settings")]
    [SerializeField][Range(0, 10)] private float cubeSize;

    [Header("Build Settings")]
    [SerializeField][Range(0, 5)] private float worldBuildDelay;
    [SerializeField][Range(0, 5)] private float superChunkBuildDelay;
    [SerializeField][Range(0, 5)] private float chunkBuildDelay;

    [Header("Perlin Noise Settings")]
    [SerializeField][Range(0, 10)] private float frequency = 0.1f;
    [SerializeField][Range(0, 50)] private float amplitude = 10f;
    [SerializeField][Range(0, 50)] private float scale = 10;

    [Header("Prefabs & Materials")]
    [SerializeField] private GameObject superChunkPrefab;
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Material material;

    private void Start()
    {
        MeshUtilities.ChangeNoiseSettings(frequency, amplitude, scale);

        StartCoroutine(BuildWorldRoutine());
    }

    private IEnumerator BuildWorldRoutine()
    {
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                for (int z = 0; z < worldSize.z; z++)
                {
                    float superChunkX = x * (superChunkSize.x * chunkSize.x * cubeSize);
                    float superChunkY = y * (superChunkSize.y * chunkSize.y * cubeSize);
                    float superChunkZ = z * (superChunkSize.z * chunkSize.z * cubeSize);
                    Vector3 superChunkPosition = transform.position + new Vector3(superChunkX, superChunkY, superChunkZ) / 2;

                    GameObject superChunk = Instantiate(superChunkPrefab, superChunkPosition, superChunkPrefab.transform.rotation, transform);
                    superChunk.name = $"SuperChunk {superChunkPosition}";
                    SuperChunk superChunkScript = superChunk.GetComponent<SuperChunk>();

                    superChunkScript.BuildSuperChunk(superChunkSize, chunkSize, cubeSize, chunkPrefab, material, superChunkBuildDelay, chunkBuildDelay);

                    yield return new WaitForSeconds(worldBuildDelay);
                }
            }
        }
    }
}