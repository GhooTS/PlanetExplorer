using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap map;
    public RuleTile ruleTile;
    [Range(0, 200)]
    public int width;
    [Range(0, 200)]
    public int height;
    [Range(1, 500)]
    public float noiseScale = 1;
    [Range(0, 1)]
    public float fillChance;
    public bool edgeOfMapAllwaysFill;
    public int edgeWidth;


    public void ClearMap()
    {
        map.ClearAllTiles();
        map.CompressBounds();
    }

    [ContextMenu("Generate Map")]
    public void GenerateMap()
    {
        ClearMap();
        Vector3Int position = Vector3Int.zero;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                float xCordingation = (float)x / width * noiseScale;
                float yCordingation = (float)y / height * noiseScale;

                float randomNum = Mathf.PerlinNoise(xCordingation, yCordingation);

                if (randomNum < fillChance || (edgeOfMapAllwaysFill && IsEgdeOfMap(x, y)))
                {
                    position.x = x;
                    position.y = y;
                    map.SetTile(position, ruleTile);
                }
            }
        }
    }

    private bool IsEgdeOfMap(int x, int y)
    {
        return x < edgeWidth || y < edgeWidth || x >= width - edgeWidth || y >= height - edgeWidth;
    }

}
