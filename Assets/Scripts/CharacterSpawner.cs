using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject wizardPrefab;
    public GameObject knightPrefab; // can leave unassigned for now, Knight isn't built

    public Transform spawnPoint;

    public static GameObject SpawnedPlayer { get; private set; }

    void Awake()
    {
        GameObject prefabToSpawn = GameData.SelectedCharacter == CharacterType.Wizard
            ? wizardPrefab
            : knightPrefab;

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No prefab assigned for selected character: " + GameData.SelectedCharacter);
            return;
        }

        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        SpawnedPlayer = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}