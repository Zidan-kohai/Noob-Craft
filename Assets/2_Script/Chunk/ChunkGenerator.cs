using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public static BlockType[,,] GenerateTerrain(float xChunkPostion, float zchunkPosition)
    {
        BlockType[,,] result = new BlockType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeigh, ChunkRenderer.ChunkDepth];

        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkDepth; z++)
            {
                float frequence = 0.15f;
                float amplituda = 5;

                float xWorldPos = x * ChunkRenderer.blockScale + xChunkPostion;
                float zWorldPos = z * ChunkRenderer.blockScale + zchunkPosition;

                float xOffset = Mathf.Sin(xWorldPos * frequence) * amplituda;
                float zOffset = Mathf.Sin(zWorldPos * frequence) * amplituda;

                float height = 50 + xOffset + zOffset; //Mathf.PerlinNoise((x + xChunkPostion) * 0.1f, (z + zchunkPosition) * 0.1f) * 5 + 15;

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlockType.Grass;
                }

            }
        }

        return result;
    }
}
