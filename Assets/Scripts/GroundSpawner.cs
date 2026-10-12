using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundSegmentPrefab;
    public Transform player;
    public float segmentWidth = 10f; // set this to match your art's actual width in world units
    public int segmentsAhead = 3; // how many segments to keep alive ahead of the player

    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = 0f;
        for (int i = 0; i < segmentsAhead; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.x + (segmentsAhead * segmentWidth) > nextSpawnX)
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        Vector3 spawnPos = new Vector3(nextSpawnX, 0f, 0f);

        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.SpawnFromPool("Ground", spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(groundSegmentPrefab, spawnPos, Quaternion.identity);
        }

        nextSpawnX += segmentWidth;
    }
}