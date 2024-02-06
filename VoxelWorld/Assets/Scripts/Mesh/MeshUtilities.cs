using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtilities
{
    // Noise settings
    private static float frequency = 0.1f; // Controls the frequency of hills and valleys
    private static float amplitude = 10f; // Controls the height of the hills and valleys
    private static float scale = 1; // Controls the scale of the noise

    public static void ChangeNoiseSettings(float frequency, float amplitude, float scale)
    {
        MeshUtilities.frequency = frequency;
        MeshUtilities.amplitude = amplitude;
        MeshUtilities.scale = scale;
    }

    public static float GenerateNoise(float x, float z)
    {
        if (scale <= 0)
        {
            scale = 0.0001f; // Avoid division by zero by setting a minimal scale value
        }

        float sineX = Mathf.Sin(x * frequency) * amplitude;
        float sineZ = Mathf.Sin(z * frequency) * amplitude;

        // Perlin noise for more natural variations
        float sampleX = x / scale * frequency;
        float sampleZ = z / scale * frequency;
        float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * amplitude;

        // Combine sine wave and Perlin noise
        float noiseValue = (sineX + sineZ + perlinValue);

        return noiseValue;
    }

    public static Mesh MergeMeshes(List<Mesh> meshesToMerge)
    {
        // Create lists for vertices, normals, uvs, and triangles
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        // Index for offsetting triangle indices
        int processedVertices = 0;

        // Process each mesh
        foreach (Mesh quadMesh in meshesToMerge)
        {
            if (quadMesh == null) continue;

            // Process mesh vertices, normals, and uvs
            vertices.AddRange(quadMesh.vertices);
            normals.AddRange(quadMesh.normals);
            uvs.AddRange(quadMesh.uv);

            // Add and offset triangle indices
            foreach (int index in quadMesh.triangles)
            {
                triangles.Add(index + processedVertices);
            }

            // Update offset for next mesh
            processedVertices += quadMesh.vertices.Length;
        }

        // Create and assign new mesh
        Mesh mergedMesh = new Mesh();
        mergedMesh.vertices = vertices.ToArray();
        mergedMesh.normals = normals.ToArray();
        mergedMesh.uv = uvs.ToArray();
        mergedMesh.triangles = triangles.ToArray();
        return mergedMesh;
    }
}
