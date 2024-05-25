using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour
{
    private const int ChunkWidth = 1;
    private const int ChunkDepth = 1;
    private const int ChunkHeigh = 1;

    private int[,,] Blocks = new int[ChunkWidth, ChunkHeigh, ChunkDepth];

    [SerializeField] private List<Vector3> verticies = new List<Vector3>();
    [SerializeField] private List<int> triangles = new List<int>();

    private void Start()
    {
        Mesh chunkMesh = new Mesh();

        for (int y = 0; y < ChunkHeigh; y++)
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for(int z = 0; z < ChunkDepth; z++)
                {
                    GenerateBlock(x, y, z);
                }
            }
        }

        chunkMesh.vertices = verticies.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        if (Blocks[x, y, z] != 0) return;

        Vector3 position = new Vector3(x, y, z);
        RightSide(position);
        LeftSide(position);
        BackSide(position);
        ForwardSide(position);
        UpSide(position);
        DawnSide(position);
    }

    private void RightSide(Vector3 position)
    {
        verticies.Add(new Vector3(0, 0, 0) + position);
        verticies.Add(new Vector3(0, 1, 0) + position);
        verticies.Add(new Vector3(0, 0, 1) + position);
        verticies.Add(new Vector3(0, 1, 1) + position);

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
    private void LeftSide(Vector3 position)
    {
        verticies.Add(new Vector3(-1, 0, 0) + position);
        verticies.Add(new Vector3(-1, 1, 0) + position);
        verticies.Add(new Vector3(-1, 0, 1) + position);
        verticies.Add(new Vector3(-1, 1, 1) + position);

        triangles.Add(verticies.Count - 2);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);

        triangles.Add(verticies.Count - 2);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
    }

    private void BackSide(Vector3 position)
    {
        verticies.Add(new Vector3(-1, 0, 0) + position);
        verticies.Add(new Vector3(-1, 1, 0) + position);
        verticies.Add(new Vector3(0, 0, 0) + position);
        verticies.Add(new Vector3(0, 1, 0) + position);

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }

    private void ForwardSide(Vector3 position)
    {
        verticies.Add(new Vector3(-1, 0, 1) + position);
        verticies.Add(new Vector3(-1, 1, 1) + position);
        verticies.Add(new Vector3(0, 0, 1) + position);
        verticies.Add(new Vector3(0, 1, 1) + position);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);
    }

    private void UpSide(Vector3 position)
    {
        verticies.Add(new Vector3(0, 1, 0) + position);
        verticies.Add(new Vector3(-1, 1, 0) + position);
        verticies.Add(new Vector3(0, 1, 1) + position);
        verticies.Add(new Vector3(-1, 1, 1) + position);

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }

    private void DawnSide(Vector3 position)
    {
        verticies.Add(new Vector3(0, 0, 0) + position);
        verticies.Add(new Vector3(-1, 0, 0) + position);
        verticies.Add(new Vector3(0, 0, 1) + position);
        verticies.Add(new Vector3(-1, 0, 1) + position);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);
    }

}
