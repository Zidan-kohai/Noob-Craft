using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    public const int ChunkWidth = 10;
    public const int ChunkDepth = 10;
    public const int ChunkHeigh = 128;
    public const int blockScale = 1;

    public ChunkSpawner Spawner;

    public ChunkData chunkData = new ChunkData();

    private List<Vector3> verticies = new List<Vector3>();
    private List<int> triangles = new List<int>();

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
        
        chunkMesh.Optimize();
        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();


        GetComponent<MeshFilter>().mesh = chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        Vector3Int blockPosition = new Vector3Int(x, y, z);
        if (GetBlockAtPosition(blockPosition) == 0) return;

        if(GetBlockAtPosition(blockPosition + Vector3Int.right) == 0) RightSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.left) == 0) LeftSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.back) == 0) BackSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0) ForwardSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.up) == 0) UpSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.down) == 0) DawnSide(blockPosition);
    }

    private BlockType GetBlockAtPosition(Vector3Int blockPosition)
    {
        if(blockPosition.x >= 0 && blockPosition.x < ChunkWidth
            && blockPosition.y >= 0 && blockPosition.y < ChunkHeigh
            && blockPosition.z >= 0 && blockPosition.z < ChunkDepth)
        {
            return chunkData.BlocksType[blockPosition.x, blockPosition.y, blockPosition.z];
        }

        if (blockPosition.y < 0 || blockPosition.y >= ChunkHeigh) return BlockType.Air;

        Vector2Int adjacentChunkPosition = chunkData.Position;

        if (blockPosition.x < 0)
        {
            adjacentChunkPosition.x--;
            blockPosition.x += ChunkWidth;
        }
        else if (blockPosition.x >= ChunkWidth)
        {
            adjacentChunkPosition.x++;
            blockPosition.x -= ChunkWidth;
        }
        else if (blockPosition.z < 0)
        {
            adjacentChunkPosition.y--;
            blockPosition.z += ChunkDepth;
        }
        else if (blockPosition.z >= ChunkDepth)
        {
            adjacentChunkPosition.y++;
            blockPosition.z -= ChunkDepth;
        }

        if (Spawner.chunks.TryGetValue(adjacentChunkPosition, out ChunkData adjacentChunk))
        {
            return adjacentChunk.BlocksType[blockPosition.x, blockPosition.y, blockPosition.z];
        }

        return BlockType.Air;
    }

    private void RightSide(Vector3 position)
    {
        

        verticies.Add((new Vector3(0, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 1, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 0, 1) + position) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + position) * blockScale);

        

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
    private void LeftSide(Vector3 position)
    {
        verticies.Add((new Vector3(-1, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 0) + position) * blockScale);
        verticies.Add((new Vector3(-1, 0, 1) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 1) + position) * blockScale);

        triangles.Add(verticies.Count - 2);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);

        triangles.Add(verticies.Count - 2);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
    }

    private void BackSide(Vector3 position)
    {
        verticies.Add((new Vector3(-1, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 1, 0) + position) * blockScale);

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }

    private void ForwardSide(Vector3 position)
    {
        verticies.Add((new Vector3(-1, 0, 1) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 1) + position) * blockScale);
        verticies.Add((new Vector3(0, 0, 1) + position) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + position) * blockScale);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);
    }

    private void UpSide(Vector3 position)
    {
        verticies.Add((new Vector3(0, 1, 0) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 1, 1) + position) * blockScale);
        verticies.Add((new Vector3(-1, 1, 1) + position) * blockScale);

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }

    private void DawnSide(Vector3 position)
    {
        verticies.Add((new Vector3(0, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(-1, 0, 0) + position) * blockScale);
        verticies.Add((new Vector3(0, 0, 1) + position) * blockScale);
        verticies.Add((new Vector3(-1, 0, 1) + position) * blockScale);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);
    }

}
