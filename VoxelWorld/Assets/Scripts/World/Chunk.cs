using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cube;

public class Chunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private Dictionary<Vector3, Cube> cubeDictionary;
    private List<Mesh> cubeMeshes;

    private Vector3 chunkSize;
    private float cubeSize;
    private Material material;

    private float buildDelay;

    public Dictionary<Vector3, Cube> CubeDictionary => cubeDictionary;

    public void BuildChunk(Vector3 chunkSize, float cubeSize, Material material, float buildDelay)
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();

        cubeDictionary = new Dictionary<Vector3, Cube>();
        cubeMeshes = new List<Mesh>();

        this.chunkSize = chunkSize;
        this.cubeSize = cubeSize;
        this.material = material;
        meshRenderer.material = material;
        this.buildDelay = buildDelay;

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

                    if (cubePosition.y == 1 || cubePosition.y <= terrainHeight)
                    {
                        cubeType = CubeTypes.Stone;
                    }
                    else
                    {
                        cubeType = CubeTypes.Air;
                    }

                    Cube cubeScript = new Cube();
                    cubeScript.SetParentChunk(this);
                    cubeScript.SetCubeType(cubeType);
                    cubeDictionary.Add(cubePosition, cubeScript);
                }
            }
        }

        foreach (KeyValuePair<Vector3, Cube> cube in cubeDictionary)
        {
            cube.Value.BuildCube(cube.Key, cubeSize, cube.Value.CubeType);
            cubeMeshes.Add(cube.Value.CubeMesh);
            yield return new WaitForSeconds(buildDelay);
        }

        // After all cubes are processed, merge them into a single mesh
        Mesh mergedMesh = MeshUtilities.MergeMeshes(cubeMeshes);
        mergedMesh.name = $"Chunk Mesh {transform.position}";
        meshFilter.mesh = mergedMesh;
    }
}
