using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtilities
{
    public static Mesh MergeQuads(List<Mesh> quadsToMerge)
    {
        // Create lists for vertices and triangles
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Index for offsetting triangle indices
        int processedVertices = 0;

        // Process each mesh
        foreach (Mesh quadMesh in quadsToMerge)
        {
            // Cycle through each vertex
            for (int i = 0; i < quadMesh.vertices.Length; i++)
            {
                // Add vertex to list
                vertices.Add(quadMesh.vertices[i]);
            }

            // Cuyxle through each triangle index
            for (int i = 0; i < quadMesh.triangles.Length; i++)
            {
                // Add triangle index to list
                triangles.Add(quadMesh.triangles[i] + processedVertices);
            }

            // Increment indesx for next mesh, to offset triangle indices
            processedVertices += quadMesh.vertices.Length;
        }

        // Create new mesh
        Mesh mergedMesh = new Mesh();

        // Assign vertices and triangles
        mergedMesh.vertices = vertices.ToArray();
        mergedMesh.triangles = triangles.ToArray();

        // Return the new mesh
        return mergedMesh;
    }
    public static Mesh MergeCubes(List<Mesh> cubesToMerge)
    {
        // Create lists for vertices and triangles
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Index for offsetting triangle indices
        int processedVertices = 0;

        // Process each mesh
        foreach (Mesh quadMesh in cubesToMerge)
        {
            // Cycle through each vertex
            for (int i = 0; i < quadMesh.vertices.Length; i++)
            {
                // Add vertex to list
                vertices.Add(quadMesh.vertices[i]);
            }

            // Cycle through each triangle index
            for (int i = 0; i < quadMesh.triangles.Length; i++)
            {
                // Add triangle index to list
                triangles.Add(quadMesh.triangles[i] + processedVertices);
            }

            // Increment index for next mesh, to offset triangle indices
            processedVertices += quadMesh.vertices.Length;
        }

        // Create new mesh
        Mesh mergedMesh = new Mesh();

        // Assign vertices and triangles
        mergedMesh.vertices = vertices.ToArray();
        mergedMesh.triangles = triangles.ToArray();

        // Return the new mesh
        return mergedMesh;
    }
}
