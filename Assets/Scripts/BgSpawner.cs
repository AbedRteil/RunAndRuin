using UnityEngine;

public class BackgroundLandmarkSpawner : MonoBehaviour
{
    [Header("Landmark Prefabs")]
    [Tooltip("Drop your Mountain prefab here. In the future, you can add your Ruined Castles here too!")]
    public GameObject[] landmarkPrefabs;

    [Header("Spawn Layout Settings")]
    public Transform cameraTransform;
    [Tooltip("How far ahead of the right screen edge should the landmark spawn?")]
    public float spawnDistanceAhead = 25f;
    [Tooltip("The vertical height for the asset. Adjust this so mountains sit cleanly on your horizon line.")]
    public float spawnYPosition = -1.5f;

    [Header("Frequency Control")]
    [Tooltip("The minimum distance (in meters/units) the player must run before a new landmark spawns.")]
    public float minDistanceBetweenSpawns = 35f;
    [Tooltip("The maximum distance the player can run before a new landmark spawns.")]
    public float maxDistanceBetweenSpawns = 60f;

    private float nextSpawnXPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        // Set the very first spawn target ahead of the camera's starting point
        CalculateNextSpawnPosition();
    }

    void Update()
    {
        // Whenever the camera crosses our planned milestone coordinate...
        if (cameraTransform.position.x >= nextSpawnXPosition)
        {
            SpawnLandmark();
            CalculateNextSpawnPosition();
        }
    }

    void CalculateNextSpawnPosition()
    {
        // Adds a random distance gap, creating unpredictable chunks of pure sky
        float randomGap = Random.Range(minDistanceBetweenSpawns, maxDistanceBetweenSpawns);
        nextSpawnXPosition = cameraTransform.position.x + randomGap;
    }

    void SpawnLandmark()
    {
        if (landmarkPrefabs == null || landmarkPrefabs.Length == 0) return;

        // 1. Pick a random prefab (Great for variety when you add castles!)
        int randomIndex = Random.Range(0, landmarkPrefabs.Length);
        GameObject selectedPrefab = landmarkPrefabs[randomIndex];

        // 2. Put it far to the right, sitting at its specified depth layer
        Vector3 spawnPosition = new Vector3(
            cameraTransform.position.x + spawnDistanceAhead,
            spawnYPosition,
            selectedPrefab.transform.position.z
        );

        // 3. Create it in your game world
        GameObject spawnedProp = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // 4. Memory Cleanup: Automatically deletes itself after 25 seconds when completely off-screen
        Destroy(spawnedProp, 25f);
    }
}
