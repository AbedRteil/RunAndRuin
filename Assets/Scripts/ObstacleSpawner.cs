using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float minReactionTime = 2.5f;
    public float maxReactionTime = 4.0f;

    private float nextSpawnX;
    private PlayerMovement playerMovement;

    public float NextSpawnX => nextSpawnX;

    void Start()
    {
        nextSpawnX = player.position.x + 20f;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player.position.x >= nextSpawnX - 10f)
        {
            SpawnObstacle();

            float currentSpeed = playerMovement != null ? Mathf.Max(playerMovement.CurrentSpeed, 1f) : 5f;

            float currentMin = currentSpeed * minReactionTime;
            float currentMax = currentSpeed * maxReactionTime;

            float randomDistance = Random.Range(currentMin, currentMax);
            nextSpawnX += randomDistance;
        }
    }

    void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(nextSpawnX, -2.5f, 0f);

        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.SpawnFromPool("Obstacle", spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}