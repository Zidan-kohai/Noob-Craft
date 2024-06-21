using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public Dictionary<Vector2Int, ChunkData> chunks = new Dictionary<Vector2Int, ChunkData>();
    public ChunkRenderer chunkPrefab;
    public InputHandler inputHandler;

    private void Start()
    {
        for (int z = 0; z < 10; z++)
        {
            for (int x = 0; x < 10; x++)
            {
                int xPos = ChunkRenderer.ChunkWidth * x * ChunkRenderer.blockScale;
                int zPos = ChunkRenderer.ChunkDepth * z * ChunkRenderer.blockScale;

                ChunkData chunkData = new ChunkData();
                chunkData.BlocksType = ChunkGenerator.GenerateTerrain(xPos, zPos);
                chunkData.Position = new Vector2Int(x, z);

                chunks.Add(new Vector2Int(x, z), chunkData);

                ChunkRenderer chunkItem = Instantiate(chunkPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
                chunkItem.ChunkData = chunkData;
                chunkItem.Spawner = this;
                
                chunkData.chunkRenderer = chunkItem;
            }
        }
    }

    private void Update()
    {
        if (inputHandler.LeftMouseButtonDown)
        {
            SpawnBlock();
        }
        else if (inputHandler.RightMouseButtonDown)
        {
            DestroyBlock();
        }
    }

    private void SpawnBlock()
    {
        if(GetBlockPositionOnSpawn(out Vector3Int blockPosition))
        {
            Vector2Int chunkPos = GetChunkByBlockPosition(blockPosition);

            if (chunks.TryGetValue(chunkPos, out ChunkData chunkData))
            {
                Vector3Int delta = new Vector3Int(chunkPos.x * ChunkRenderer.ChunkWidth, 0, chunkPos.y * ChunkRenderer.ChunkDepth);
                chunkData.chunkRenderer.SpawnBlock(blockPosition - delta);
            }
            else
            {
                Debug.Log("Can`t get chunk positon");
            }
        }
        else
        {
            Debug.Log("Can`t get block positon");
        }
    }

    private void DestroyBlock()
    {
        if (GetBlockPositionOnDestroy(out Vector3Int blockPosition))
        {
            Vector2Int chunkPos = GetChunkByBlockPosition(blockPosition);

            if (chunks.TryGetValue(chunkPos, out ChunkData chunkData))
            {
                Vector3Int delta = new Vector3Int(chunkPos.x * ChunkRenderer.ChunkWidth, 0, chunkPos.y * ChunkRenderer.ChunkDepth);
                chunkData.chunkRenderer.DestroyBlock(blockPosition - delta);
            }
            else
            {
                Debug.Log("Can`t get chunk positon");
            }
        }
        else
        {
            Debug.Log("Can`t get block positon");
        }
    }

    private bool GetBlockPositionOnSpawn(out Vector3Int blockPosition)
    {
        bool result = false;
        blockPosition = Vector3Int.zero;

        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            result = true;

            Vector3 WorldPosition = hit.point + hit.normal * ChunkRenderer.blockScale / 2;
            blockPosition = Vector3Int.FloorToInt(WorldPosition / ChunkRenderer.blockScale);

        }

        return result;
    }

    private bool GetBlockPositionOnDestroy(out Vector3Int blockPosition)
    {
        bool result = false;
        blockPosition = Vector3Int.zero;

        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            result = true;

            Vector3 WorldPosition = hit.point - hit.normal * ChunkRenderer.blockScale / 2;
            blockPosition = Vector3Int.FloorToInt(WorldPosition / ChunkRenderer.blockScale);

        }

        return result;
    }

    private Vector2Int GetChunkByBlockPosition(Vector3Int blockPosition)
    {
        return new Vector2Int(blockPosition.x / ChunkRenderer.ChunkWidth, blockPosition.z / ChunkRenderer.ChunkDepth);
    }
}
