using System;
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

    // Builds a single triangle mesh in the specified direction
    public static Mesh BuildTriangleMesh(Vector3 positionOffset, MeshDirection direction, float triangleSize)
    {
        // Vertex offset
        float offset = 0.5f * triangleSize;

        Vector3[] vertices;
        Vector3[] normals;

        // Vertex positions
        if (direction == MeshDirection.Top)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Top face
                new Vector3(offset, offset, offset) + positionOffset, // Position 0
                new Vector3(offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(-offset, offset, offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 1, 0), // Normal 0
                new Vector3(0, 1, 0), // Normal 1
                new Vector3(0, 1, 0) // Normal 2
            };
        }
        else if (direction == MeshDirection.Bottom)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Bottom face
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, -1, 0), // Normal 0
                new Vector3(0, -1, 0), // Normal 1
                new Vector3(0, -1, 0) // Normal 2
            };
        }
        else if (direction == MeshDirection.Left)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Left face
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(-offset, offset, offset) + positionOffset, // Position 1
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(-1, 0, 0), // Normal 0
                new Vector3(-1, 0, 0), // Normal 1
                new Vector3(-1, 0, 0) // Normal 2
            };
        }
        else if (direction == MeshDirection.Right)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Right face
                new Vector3(offset, -offset, -offset) + positionOffset, // Position 0
                new Vector3(offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(1, 0, 0), // Normal 0
                new Vector3(1, 0, 0), // Normal 1
                new Vector3(1, 0, 0) // Normal 2
            };
        }
        else if (direction == MeshDirection.Front)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Front face
                new Vector3(offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(offset, offset, offset) + positionOffset, // Position 1
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 0, 1), // Normal 0
                new Vector3(0, 0, 1), // Normal 1
                new Vector3(0, 0, 1) // Normal 2
            };
        }
        else if (direction == MeshDirection.Back)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Back face
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 0
                new Vector3(-offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, -offset) + positionOffset, // Position 2
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 0, -1), // Normal 0
                new Vector3(0, 0, -1), // Normal 1
                new Vector3(0, 0, -1) // Normal 2
            };
        }
        else
        {
            Debug.Log("Error building triangle mesh");
            return new Mesh();
        }

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0), // UV 0
            new Vector2(0, 1), // UV 1
            new Vector2(1, 0) // UV 2
        };

        // Triangle indices, clockwise for front facing
        int[] trianglePositions = new int[]
        {
            0, 1, 2, // Triangle 0
        };

        // Create new mesh
        Mesh triangleMesh = new Mesh();

        // Assign vertices and triangles to the new mesh
        triangleMesh.vertices = vertices;
        triangleMesh.normals = normals;
        triangleMesh.uv = uvs;
        triangleMesh.triangles = trianglePositions;

        // Return the new mesh
        return triangleMesh;
    }

    // Builds a single quad mesh in the specified direction
    public static Mesh BuildQuadMesh(Vector3 positionOffset, MeshDirection direction, float quadSize)
    {
        // Vertex offset
        float offset = 0.5f * quadSize;

        Vector3[] vertices;
        Vector3[] normals;

        // Vertex positions
        if (direction == MeshDirection.Top)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Top face
                new Vector3(offset, offset, offset) + positionOffset, // Position 0
                new Vector3(offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(-offset, offset, offset) + positionOffset, // Position 2
                new Vector3(-offset, offset, -offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 1, 0), // Normal 0
                new Vector3(0, 1, 0), // Normal 1
                new Vector3(0, 1, 0), // Normal 2
                new Vector3(0, 1, 0) // Normal 3
            };
        }
        else if (direction == MeshDirection.Bottom)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Bottom face
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, offset) + positionOffset, // Position 2
                new Vector3(offset, -offset, -offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, -1, 0), // Normal 0
                new Vector3(0, -1, 0), // Normal 1
                new Vector3(0, -1, 0), // Normal 2
                new Vector3(0, -1, 0) // Normal 3
            };
        }
        else if (direction == MeshDirection.Left)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Left face
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(-offset, offset, offset) + positionOffset, // Position 1
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 2
                new Vector3(-offset, offset, -offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(-1, 0, 0), // Normal 0
                new Vector3(-1, 0, 0), // Normal 1
                new Vector3(-1, 0, 0), // Normal 2
                new Vector3(-1, 0, 0) // Normal 3
            };
        }
        else if (direction == MeshDirection.Right)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Right face
                new Vector3(offset, -offset, -offset) + positionOffset, // Position 0
                new Vector3(offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, offset) + positionOffset, // Position 2
                new Vector3(offset, offset, offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(1, 0, 0), // Normal 0
                new Vector3(1, 0, 0), // Normal 1
                new Vector3(1, 0, 0), // Normal 2
                new Vector3(1, 0, 0) // Normal 3
            };
        }
        else if (direction == MeshDirection.Front)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Front face
                new Vector3(offset, -offset, offset) + positionOffset, // Position 0
                new Vector3(offset, offset, offset) + positionOffset, // Position 1
                new Vector3(-offset, -offset, offset) + positionOffset, // Position 2
                new Vector3(-offset, offset, offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 0, 1), // Normal 0
                new Vector3(0, 0, 1), // Normal 1
                new Vector3(0, 0, 1), // Normal 2
                new Vector3(0, 0, 1) // Normal 3
            };
        }
        else if (direction == MeshDirection.Back)
        {
            // Vertex positions
            vertices = new Vector3[]
            {
                // Back face
                new Vector3(-offset, -offset, -offset) + positionOffset, // Position 0
                new Vector3(-offset, offset, -offset) + positionOffset, // Position 1
                new Vector3(offset, -offset, -offset) + positionOffset, // Position 2
                new Vector3(offset, offset, -offset) + positionOffset, // Position 3
            };

            // Normals
            normals = new Vector3[]
            {
                new Vector3(0, 0, -1), // Normal 0
                new Vector3(0, 0, -1), // Normal 1
                new Vector3(0, 0, -1), // Normal 2
                new Vector3(0, 0, -1) // Normal 3
            };
        }
        else
        {
            Debug.Log("Error building quad mesh");
            return new Mesh();
        }

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0), // UV 0
            new Vector2(0, 1), // UV 1
            new Vector2(1, 0), // UV 2
            new Vector2(1, 1) // UV 3
        };

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
        quadMesh.normals = normals;
        quadMesh.uv = uvs;
        quadMesh.triangles = trianglePositions;

        // Return the new mesh
        return quadMesh;
    }

    // Builds a single cube mesh
    public static Mesh BuildCubeMesh(Vector3 positionOffset, float cubeSize)
    {
        // Vertex offset
        float vertexOffset = 0.5f * cubeSize;

        // Vertex positions
        Vector3[] vertices = new Vector3[]
        {
            // Front face
            new Vector3(vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 0
            new Vector3(vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 1
            new Vector3(-vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 2
            new Vector3(-vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 3

            // Back face
            new Vector3(-vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 4
            new Vector3(-vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 5
            new Vector3(vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 6
            new Vector3(vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 7

            // Left face
            new Vector3(-vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 8
            new Vector3(-vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 9
            new Vector3(-vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 10
            new Vector3(-vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 11

            // Right face
            new Vector3(vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 12
            new Vector3(vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 13
            new Vector3(vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 14
            new Vector3(vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 15

            // Top face
            new Vector3(vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 16
            new Vector3(vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 17
            new Vector3(-vertexOffset, vertexOffset, vertexOffset) + positionOffset, // Position 18
            new Vector3(-vertexOffset, vertexOffset, -vertexOffset) + positionOffset, // Position 19

            // Bottom face
            new Vector3(-vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 20
            new Vector3(-vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 21
            new Vector3(vertexOffset, -vertexOffset, vertexOffset) + positionOffset, // Position 22
            new Vector3(vertexOffset, -vertexOffset, -vertexOffset) + positionOffset, // Position 23
        };

        Vector3[] normals = new Vector3[]
        {
            // Front face
            new Vector3(0, 0, 1), // Normal 0
            new Vector3(0, 0, 1), // Normal 1
            new Vector3(0, 0, 1), // Normal 2
            new Vector3(0, 0, 1), // Normal 3

            // Back face
            new Vector3(0, 0, -1), // Normal 4
            new Vector3(0, 0, -1), // Normal 5
            new Vector3(0, 0, -1), // Normal 6
            new Vector3(0, 0, -1), // Normal 7

            // Left face
            new Vector3(-1, 0, 0), // Normal 8
            new Vector3(-1, 0, 0), // Normal 9
            new Vector3(-1, 0, 0), // Normal 10
            new Vector3(-1, 0, 0), // Normal 11

            // Right face
            new Vector3(1, 0, 0), // Normal 12
            new Vector3(1, 0, 0), // Normal 13
            new Vector3(1, 0, 0), // Normal 14
            new Vector3(1, 0, 0), // Normal 15

            // Top face
            new Vector3(0, 1, 0), // Normal 16
            new Vector3(0, 1, 0), // Normal 17
            new Vector3(0, 1, 0), // Normal 18
            new Vector3(0, 1, 0), // Normal 19

            // Bottom face
            new Vector3(0, -1, 0), // Normal 20
            new Vector3(0, -1, 0), // Normal 21
            new Vector3(0, -1, 0), // Normal 22
            new Vector3(0, -1, 0) // Normal 23
        };

        Vector2[] uvs = new Vector2[]
        {
            // Front face
            new Vector2(0, 0), // UV 0
            new Vector2(0, 1), // UV 1
            new Vector2(1, 0), // UV 2
            new Vector2(1, 1), // UV 3

            // Back face
            new Vector2(0, 0), // UV 4
            new Vector2(0, 1), // UV 5
            new Vector2(1, 0), // UV 6
            new Vector2(1, 1), // UV 7

            // Left face
            new Vector2(0, 0), // UV 8
            new Vector2(0, 1), // UV 9
            new Vector2(1, 0), // UV 10
            new Vector2(1, 1), // UV 11

            // Right face
            new Vector2(0, 0), // UV 12
            new Vector2(0, 1), // UV 13
            new Vector2(1, 0), // UV 14
            new Vector2(1, 1), // UV 15

            // Top face
            new Vector2(0, 0), // UV 16
            new Vector2(0, 1), // UV 17
            new Vector2(1, 0), // UV 18
            new Vector2(1, 1), // UV 19

            // Bottom face
            new Vector2(0, 0), // UV 20
            new Vector2(0, 1), // UV 21
            new Vector2(1, 0), // UV 22
            new Vector2(1, 1) // UV 23
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
        cubeMesh.normals = normals;
        cubeMesh.uv = uvs;
        cubeMesh.triangles = trianglePositions;

        // Return the new mesh
        return cubeMesh;
    }
}
