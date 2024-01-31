using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public enum MeshDirection
    {
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back
    }
    public enum Meshtype
    {
        Triangle,
        Quad,
        Cube,
        Chunk
    }

    public static Mesh BuildTriangleMesh(Vector3 worldPosition, MeshDirection direction, float triangleSize)
    {
        // Vertex offset
        float offset = 0.5f * triangleSize;

        Vector3[] vertices;

        // Vertex positions
        if (direction == MeshDirection.Top)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Top face
                new Vector3(offset, offset, offset) + worldPosition, // Position 0
                new Vector3(offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(-offset, offset, offset) + worldPosition, // Position 2
            };
        }
        else if (direction == MeshDirection.Bottom)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Bottom face
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, offset) + worldPosition, // Position 2
            };
        }
        else if (direction == MeshDirection.Left)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Left face
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(-offset, offset, offset) + worldPosition, // Position 1
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 2
            };
        }
        else if (direction == MeshDirection.Right)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Right face
                new Vector3(offset, -offset, -offset) + worldPosition, // Position 0
                new Vector3(offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, offset) + worldPosition, // Position 2
            };
        }
        else if (direction == MeshDirection.Front)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Front face
                new Vector3(offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(offset, offset, offset) + worldPosition, // Position 1
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 2
            };
        }
        else if (direction == MeshDirection.Back)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Back face
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 0
                new Vector3(-offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, -offset) + worldPosition, // Position 2
            };
        }
        else
        {
            Debug.Log("Error building triangle mesh");
            return new Mesh();
        }

        // Triangle indices, clockwise for front facing
        int[] trianglePositions = new int[]
        {
            0, 1, 2, // Triangle 0
        };

        // Create new mesh
        Mesh triangleMesh = new Mesh();

        // Assign vertices and triangles to the new mesh
        triangleMesh.vertices = vertices;
        triangleMesh.triangles = trianglePositions;

        // Return the new mesh
        return triangleMesh;
    }
    public static Mesh BuildQuadMesh(Vector3 worldPosition, MeshDirection direction, float quadSize)
    {
        // Vertex offset
        float offset = 0.5f * quadSize;

        Vector3[] vertices;

        // Vertex positions
        if (direction == MeshDirection.Top)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Top face
                new Vector3(offset, offset, offset) + worldPosition, // Position 0
                new Vector3(offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(-offset, offset, offset) + worldPosition, // Position 2
                new Vector3(-offset, offset, -offset) + worldPosition, // Position 3
            };
        }
        else if (direction == MeshDirection.Bottom)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Bottom face
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, offset) + worldPosition, // Position 2
                new Vector3(offset, -offset, -offset) + worldPosition, // Position 3
            };
        }
        else if (direction == MeshDirection.Left)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Left face
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(-offset, offset, offset) + worldPosition, // Position 1
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 2
                new Vector3(-offset, offset, -offset) + worldPosition, // Position 3
            };
        }
        else if (direction == MeshDirection.Right)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Right face
                new Vector3(offset, -offset, -offset) + worldPosition, // Position 0
                new Vector3(offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, offset) + worldPosition, // Position 2
                new Vector3(offset, offset, offset) + worldPosition, // Position 3
            };
        }
        else if (direction == MeshDirection.Front)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Front face
                new Vector3(offset, -offset, offset) + worldPosition, // Position 0
                new Vector3(offset, offset, offset) + worldPosition, // Position 1
                new Vector3(-offset, -offset, offset) + worldPosition, // Position 2
                new Vector3(-offset, offset, offset) + worldPosition, // Position 3
            };
        }
        else if (direction == MeshDirection.Back)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Back face
                new Vector3(-offset, -offset, -offset) + worldPosition, // Position 0
                new Vector3(-offset, offset, -offset) + worldPosition, // Position 1
                new Vector3(offset, -offset, -offset) + worldPosition, // Position 2
                new Vector3(offset, offset, -offset) + worldPosition, // Position 3
            };
        }
        else
        {
            Debug.Log("Error building quad mesh");
            return new Mesh();
        }

        // Triangle indices, clockwise for front facing
        int[] trianglePositions = new int[]
        {
            0, 1, 2, // Triangle 0
            2, 1, 3 // Triangle 1
        };

        // Create new mesh
        Mesh quadMesh = new Mesh();

        // Assign vertices and triangles to the new mesh
        quadMesh.vertices = vertices;
        quadMesh.triangles = trianglePositions;

        // Return the new mesh
        return quadMesh;
    }
    public static Mesh BuildCubeMesh(Vector3 worldPosition, float cubeSize)
    {
        // Vertex offset
        float offset = 0.5f * cubeSize;

        // Vertex positions
        Vector3[] vertices = new Vector3[]
        {
            // Front face
            new Vector3(offset, -offset, offset) + worldPosition, // Position 0
            new Vector3(offset, offset, offset) + worldPosition, // Position 1
            new Vector3(-offset, -offset, offset) + worldPosition, // Position 2
            new Vector3(-offset, offset, offset) + worldPosition, // Position 3

            // Back face
            new Vector3(-offset, -offset, -offset) + worldPosition, // Position 4
            new Vector3(-offset, offset, -offset) + worldPosition, // Position 5
            new Vector3(offset, -offset, -offset) + worldPosition, // Position 6
            new Vector3(offset, offset, -offset) + worldPosition, // Position 7

            // Left face
            new Vector3(-offset, -offset, offset) + worldPosition, // Position 8
            new Vector3(-offset, offset, offset) + worldPosition, // Position 9
            new Vector3(-offset, -offset, -offset) + worldPosition, // Position 10
            new Vector3(-offset, offset, -offset) + worldPosition, // Position 11

            // Right face
            new Vector3(offset, -offset, -offset) + worldPosition, // Position 12
            new Vector3(offset, offset, -offset) + worldPosition, // Position 13
            new Vector3(offset, -offset, offset) + worldPosition, // Position 14
            new Vector3(offset, offset, offset) + worldPosition, // Position 15

            // Top face
            new Vector3(offset, offset, offset) + worldPosition, // Position 16
            new Vector3(offset, offset, -offset) + worldPosition, // Position 17
            new Vector3(-offset, offset, offset) + worldPosition, // Position 18
            new Vector3(-offset, offset, -offset) + worldPosition, // Position 19

            // Bottom face
            new Vector3(-offset, -offset, offset) + worldPosition, // Position 20
            new Vector3(-offset, -offset, -offset) + worldPosition, // Position 21
            new Vector3(offset, -offset, offset) + worldPosition, // Position 22
            new Vector3(offset, -offset, -offset) + worldPosition, // Position 23
        };

        // Triangle indices, clockwise for front facing
        int[] trianglePositions = new int[]
        {
            // Front face
            0, 1, 2, // Triangle 0
            2, 1, 3, // Triangle 1

            // Back face
            4, 5, 6, // Triangle 2
            6, 5, 7, // Triangle 3

            // Left face
            8, 9, 10, // Triangle 4
            10, 9, 11, // Triangle 5

            // Right face
            12, 13, 14, // Triangle 6
            14, 13, 15, // Triangle 7

            // Top face
            16, 17, 18, // Triangle 8
            18, 17, 19, // Triangle 9

            // Bottom face
            20, 21, 22, // Triangle 10
            22, 21, 23 // Triangle 11
        };

        // Create new mesh
        Mesh cubeMesh = new Mesh();

        // Assign vertices and triangles to the new mesh
        cubeMesh.vertices = vertices;
        cubeMesh.triangles = trianglePositions;

        // Return the new mesh
        return cubeMesh;
    }
    public static Mesh BuildChunkMesh(Vector3 worldPosition, Vector3 chunkSize, float cubeSize)
    {
        // Create a list of cubes to merge
        List<Mesh> cubesToMerge = new List<Mesh>();

        // Cycle through each cube
        for (int x = 0; x < chunkSize.x; x++)
        {
            for (int y = 0; y < chunkSize.y; y++)
            {
                for (int z = 0; z < chunkSize.z; z++)
                {
                    // Create a cube mesh
                    Mesh cubeMesh = BuildCubeMesh(new Vector3(x, y, z) + worldPosition, cubeSize);

                    // Add the cube mesh to the list
                    cubesToMerge.Add(cubeMesh);
                }
            }
        }

        // Merge the cubes
        Mesh mergedMesh = MeshUtilities.MergeCubes(cubesToMerge);

        return mergedMesh;
    }
}
