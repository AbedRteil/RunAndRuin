using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInfinity : MonoBehaviour
{
    public Transform player;
    public TileBase groundTile;

    public int tilesAhead = 50;
    public int groundY = -1;

    private Tilemap tilemap;
    private int lastGeneratedX;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        int playerX = Mathf.FloorToInt(player.position.x);

        lastGeneratedX = playerX;

        GenerateGround(playerX, playerX + tilesAhead);
    }

    void Update()
    {
        int playerX = Mathf.FloorToInt(player.position.x);

        // Generate BEFORE the player reaches the end
        if (playerX + 20 > lastGeneratedX)
        {
            GenerateGround(lastGeneratedX, playerX + tilesAhead);
        }
    }

    void GenerateGround(int startX, int endX)
    {
        for (int x = startX; x <= endX; x++)
        {
            Vector3Int position = new Vector3Int(x, groundY, 0);

            if (!tilemap.HasTile(position))
            {
                tilemap.SetTile(position, groundTile);
            }
        }

        lastGeneratedX = endX;
    }
}