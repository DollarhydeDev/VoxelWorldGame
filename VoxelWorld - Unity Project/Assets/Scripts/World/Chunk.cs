using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cube;

public class Chunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private Dictionary<Vector3, Cube> cubeDictionary;
    private List<Mesh> cubeMeshes;

    private Vector3 chunkSize;
    private float cubeSize;
    private Material material;

    private float buildDelay;

    private World world;

    public Dictionary<Vector3, Cube> CubeDictionary => cubeDictionary;

    public void BuildChunk(Vector3 chunkSize, float cubeSize, Material material, float buildDelay)
    {
        // Add the needed components to render the chunk.
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        world = FindFirstObjectByType<World>();

        // Dictionary to store the cubes (and their position) and a list to store the meshes.
        cubeDictionary = new Dictionary<Vector3, Cube>();
        cubeMeshes = new List<Mesh>();

        // Settings passed from the SuperChunk class.
        this.chunkSize = chunkSize;
        this.cubeSize = cubeSize;
        this.material = material;
        this.buildDelay = buildDelay;
        meshRenderer.material = material;

        // Coroutine to build the chunk.
        StartCoroutine(BuildChunkRoutine());
    }
    private IEnumerator BuildChunkRoutine()
    {
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                for (int z = 0; z < chunkSize.z; z++)
                {
                    // Calculate the position of the cube
                    float cubeX = x * cubeSize;
                    float cubeY = y * cubeSize;
                    float cubeZ = z * cubeSize;
                    Vector3 cubePosition = transform.position + new Vector3(cubeX, cubeY, cubeZ);

                    // Calculate the noise value for the current x and z to determine the terrain height
                    float terrainHeight = MeshUtilities.GenerateNoise(cubePosition.x, cubePosition.z);

                    // Set the cube type based on the terrain height
                    CubeTypes cubeType;

                    // TODO: Add more cube types

                    // If the cube is at 1, or is below the terrain height, it's stone. Otherwise, it's air.
                    if (cubePosition.y == 1 || cubePosition.y <= terrainHeight)
                    {
                        cubeType = CubeTypes.Stone;
                    }
                    else
                    {
                        cubeType = CubeTypes.Air;
                    }

                    // Initialize the cube and add it to the dictionary. Set the parent chunk and the cube type, to be used when it checks for neighbors.
                    Cube cubeScript = new Cube();
                    cubeScript.SetParentChunk(this);
                    cubeScript.SetCubeType(cubeType);
                    cubeDictionary.Add(cubePosition, cubeScript);
                }
            }
        }

        // Loop through the dictionary and build a mesh for each of the cubes
        foreach (KeyValuePair<Vector3, Cube> cube in cubeDictionary)
        {
            cube.Value.BuildCube(cube.Key, cubeSize, cube.Value.CubeType);
            cubeMeshes.Add(cube.Value.CubeMesh);
            yield return new WaitForSeconds(buildDelay);
        }

        // After all cubes are processed, merge them into a single mesh using the mesh list
        Mesh mergedMesh = MeshUtilities.MergeMeshes(cubeMeshes);
        mergedMesh.name = $"Chunk Mesh {transform.position}";

        // Recalculate mesh bounds for the mesh collider to work properly
        mergedMesh.RecalculateBounds();
        meshCollider.sharedMesh = mergedMesh;

        meshFilter.mesh = mergedMesh;

        // Increment the loading bar
        world.IncrementLoadingBar();
    }
}
