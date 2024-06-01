using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public Dictionary<Vector2Int, ChunkData> chunks = new Dictionary<Vector2Int, ChunkData>();
    public ChunkRenderer chunkPrefab;

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
                chunkItem.chunkData = chunkData;
                chunkItem.Spawner = this;
            }
        }

    }
}
