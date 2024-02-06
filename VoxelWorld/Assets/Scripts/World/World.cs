using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject superChunkPrefab;
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Material material;

    [Header("References")]
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TMP_Text loadingText;

    private void Start()
    {
        loadingBar.gameObject.SetActive(true);
        loadingBar.maxValue = (worldSize.x * superChunkSize.x) * (worldSize.y * superChunkSize.y) * (worldSize.z * superChunkSize.z);
        loadingBar.value = 0;

        // Update the terrain noise settings with the values in the inspector.
        MeshUtilities.ChangeNoiseSettings(frequency, amplitude, scale);

        // Coroutine to build the world.
        StartCoroutine(BuildWorldRoutine());

        // Coroutine to update the loading text and spawn the player.
        StartCoroutine(UpdateLoadingText());
    }

    private IEnumerator BuildWorldRoutine()
    {
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                for (int z = 0; z < worldSize.z; z++)
                {
                    // Calculate the position of the super chunk.
                    float superChunkX = x * (superChunkSize.x * chunkSize.x * cubeSize);
                    float superChunkY = y * (superChunkSize.y * chunkSize.y * cubeSize);
                    float superChunkZ = z * (superChunkSize.z * chunkSize.z * cubeSize);
                    Vector3 superChunkPosition = transform.position + new Vector3(superChunkX, superChunkY, superChunkZ) / 2; // Divide by 2 to center the super chunks.

                    // Instantiate the super chunk and build it.
                    GameObject superChunk = Instantiate(superChunkPrefab, superChunkPosition, superChunkPrefab.transform.rotation, transform);
                    superChunk.name = $"SuperChunk {superChunkPosition}";
                    SuperChunk superChunkScript = superChunk.GetComponent<SuperChunk>();
                    superChunkScript.BuildSuperChunk(superChunkSize, chunkSize, cubeSize, chunkPrefab, material, superChunkBuildDelay, chunkBuildDelay);

                    // Delay time between building super chunks.
                    yield return new WaitForSeconds(worldBuildDelay);
                }
            }
        }
    }
    private IEnumerator UpdateLoadingText()
    {
        while (loadingBar.value < loadingBar.maxValue)
        {
            loadingText.text = "Loading World";
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading World.";
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading World..";
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading World...";
            yield return new WaitForSeconds(1f);
        }

        loadingText.text = "Loading Complete";
        yield return new WaitForSeconds(1f);
        loadingBar.gameObject.SetActive(false);

        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        // Calculate the center of the world. Use noise to determine the Y position, since I also use this noise to determine the terrain height.
        float worldX = worldSize.x * superChunkSize.x * chunkSize.x * cubeSize / 2;
        float worldZ = worldSize.z * superChunkSize.z * chunkSize.z * cubeSize / 2;
        float worldY = MeshUtilities.GenerateNoise(worldX, worldZ) + 5; // Add 5 to the Y position to prevent the player from spawning inside the terrain.

        // Spawn the player.
        Vector3 worldCenter = transform.position + new Vector3(worldX, worldY + 2, worldZ);
        Instantiate(playerPrefab, worldCenter, playerPrefab.transform.rotation);
    }
    public void IncrementLoadingBar()
    {
        if (loadingBar.value < loadingBar.maxValue)
        {
            loadingBar.value++;
        }
    }
}