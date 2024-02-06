using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    public Mesh CubeMesh { get; private set; }
    public Vector3 CubePosition { get; private set; }
    public float CubeSize { get; private set; }
    public CubeTypes CubeType { get; private set; }

    private Chunk parentChunk;

    private bool hasAnyNeighbors = false;

    private bool hasTopNeighbor = false;
    private bool hasBottomNeighbor = false;
    private bool hasLeftNeighbor = false;
    private bool hasRightNeighbor = false;
    private bool hasFrontNeighbor = false;
    private bool hasBackNeighbor = false;

    public enum CubeTypes
    {
        Air,
        Stone
    }

    public void SetParentChunk(Chunk parentChunk)
    {
        this.parentChunk = parentChunk;
    }
    public void SetCubeType(CubeTypes cubeType)
    {
        CubeType = cubeType;
    }
    public void BuildCube(Vector3 cubePosition, float cubeSize, CubeTypes cubeType)
    {
        CubePosition = cubePosition;
        CubeSize = cubeSize;
        CubeType = cubeType;

        // TODO: Review why some meshes are still being built even when it has neighbors

        // Skip Air cubes for now
        if (cubeType != CubeTypes.Air)
        {
            CheckForNeighbors();

            // The cube has no neighbors, so build a full cube mesh. (Unless its air, then don't build a mesh)
            if (!hasAnyNeighbors)
            {
                CubeMesh = MeshGenerator.BuildCubeMesh(CubePosition, CubeSize);
            }
            // The cube has some neighbors, so build a quad mesh for each of those directions.
            else
            {
                List<Mesh> faceMeshes = new List<Mesh>();

                // Build a mesh for each face that has no neighbor and add it to the faceMeshes list
                if (!hasTopNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Top, CubeSize));
                if (!hasBottomNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Bottom, CubeSize));
                if (!hasLeftNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Left, CubeSize));
                if (!hasRightNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Right, CubeSize));
                if (!hasFrontNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Front, CubeSize));
                if (!hasBackNeighbor) faceMeshes.Add(MeshGenerator.BuildQuadMesh(CubePosition, MeshGenerator.MeshDirection.Back, CubeSize));

                // Merge the face meshes into a single cube mesh
                CubeMesh = MeshUtilities.MergeMeshes(faceMeshes);
            }
        }
    }

    private void CheckForNeighbors()
    {
        // Directions to check for neighbors
        Vector3[] directions = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        // Cycle through each direction and check if a neighbor exists
        foreach (Vector3 direction in directions)
        {
            // Calculate the position of the neighbor
            Vector3 neighborPosition = CubePosition * CubeSize + direction;

            // Check if the neighbor exists in the parent chunk
            if (parentChunk.CubeDictionary.ContainsKey(neighborPosition))
            {
                // make sure its not air
                if (parentChunk.CubeDictionary[neighborPosition].CubeType != CubeTypes.Air)
                {
                    // A neighbor exists, so set the hasAnyNeighbors flag to true
                    if (!hasAnyNeighbors) hasAnyNeighbors = true;

                    // Determine the direction of the neighbor and set the corresponding flag
                    if (direction == Vector3.up) hasTopNeighbor = true;
                    else if (direction == Vector3.down) hasBottomNeighbor = true;
                    else if (direction == Vector3.left) hasLeftNeighbor = true;
                    else if (direction == Vector3.right) hasRightNeighbor = true;
                    else if (direction == Vector3.forward) hasFrontNeighbor = true;
                    else if (direction == Vector3.back) hasBackNeighbor = true;
                }
            }
        }
    }
}