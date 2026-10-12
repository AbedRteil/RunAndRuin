using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public ObstacleSpawner obstacleSpawner;

    public float minReactionTime = 1.8f;
    public float maxReactionTime = 2.8f;

    public float minHeight = 0f;
    public float maxHeight = 4f;

    [Range(0f, 1f)]
    public float comboChance = 0.2f;

    private float nextSpawnX;
    private PlayerMovement playerMovement;

    void Start()
    {
        nextSpawnX = player.position.x + 30f;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player.position.x >= nextSpawnX - 10f)
        {
            SpawnEnemy();

            float currentSpeed = playerMovement != null ? Mathf.Max(playerMovement.CurrentSpeed, 1f) : 5f;

            float currentMin = currentSpeed * minReactionTime;
            float currentMax = currentSpeed * maxReactionTime;

            float randomDistance = Random.Range(currentMin, currentMax);
            nextSpawnX += randomDistance;
        }
    }

    void SpawnEnemy()
    {
        float spawnX = nextSpawnX;

        if (obstacleSpawner != null && Random.value < comboChance)
        {
            spawnX = obstacleSpawner.NextSpawnX + Random.Range(-1.5f, 1.5f);
        }

        float spawnY = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.SpawnFromPool("Enemy", spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}