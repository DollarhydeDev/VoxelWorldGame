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
    public static Mesh MergeCubesClean(List<Mesh> cubesToMerge)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Dictionary to map from old vertex indices to new indices
        Dictionary<int, int> vertexMapping = new Dictionary<int, int>();

        // Process meshes
        foreach (Mesh cubeMesh in cubesToMerge)
        {
            // Loop through vertices
            for (int i = 0; i < cubeMesh.vertices.Length; i++)
            {
                Vector3 vertex = cubeMesh.vertices[i];

                // Check if vertex already exists
                if (!vertices.Contains(vertex))
                {
                    // if it doesn't, add it to the list and map the index
                    vertices.Add(vertex);
                    vertexMapping[i] = vertices.Count - 1;
                }
                else
                {
                    // if it does, retrieve the index from the existing vertex
                    vertexMapping[i] = vertices.IndexOf(vertex);
                }
            }

            // Adjust the triangle indices based on the vertex mapping
            for (int i = 0; i < cubeMesh.triangles.Length; i++)
            {
                int oldVertexIndex = cubeMesh.triangles[i];
                int newVertexIndex = vertexMapping[oldVertexIndex];
                triangles.Add(newVertexIndex);
            }
        }

        Mesh mergedMesh = new Mesh();
        mergedMesh.vertices = vertices.ToArray();
        mergedMesh.triangles = triangles.ToArray();

        return mergedMesh;
    }
}
