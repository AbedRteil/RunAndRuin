using UnityEngine;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public static SoulManager Instance;
    public GameObject soulIconPrefab;
    public RectTransform soulUITarget; // the souls counter's RectTransform
    public Canvas gameplayCanvas;


    public TextMeshProUGUI runSoulsText; // shows souls collected THIS run (gameplay screen)

    private int runSouls;   // resets every run
    private int totalSouls; // persists forever across runs

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalSouls = PlayerPrefs.GetInt("TotalSouls", 0);
        runSouls = 0;
        UpdateRunSoulsUI();
    }

    void Update()
    {
        // DEV ONLY: Press T to reset total souls (for testing)
        if (Input.GetKeyDown(KeyCode.T))
        {
            totalSouls = 0;
            PlayerPrefs.DeleteKey("TotalSouls");
            PlayerPrefs.Save();
            Debug.Log("Total Souls Reset!");
        }
    }
    public void SpawnSoulPickup(Vector3 worldPosition)
{
    if (soulIconPrefab == null || soulUITarget == null || gameplayCanvas == null) return;

    Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);

    GameObject icon = Instantiate(soulIconPrefab, gameplayCanvas.transform);
    RectTransform iconRect = icon.GetComponent<RectTransform>();

    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        gameplayCanvas.transform as RectTransform,
        screenPoint,
        gameplayCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
        out Vector2 localPoint
    );

    iconRect.anchoredPosition = localPoint;

    SoulPickupEffect effect = icon.GetComponent<SoulPickupEffect>();
    if (effect == null)
    {
        effect = icon.AddComponent<SoulPickupEffect>();
    }
    effect.Init(soulUITarget);
}

    public void AddSouls(int amount)
    {
        runSouls += amount;
        UpdateRunSoulsUI();
    }

    // Call this once, at the moment of death, to bank the run's souls into the total
    public void BankRunSouls()
    {
        totalSouls += runSouls;
        PlayerPrefs.SetInt("TotalSouls", totalSouls);
        PlayerPrefs.Save();
    }

    public int GetRunSouls()
    {
        return runSouls;
    }

    public int GetTotalSouls()
    {
        return totalSouls;
    }

    private void UpdateRunSoulsUI()
    {
        if (runSoulsText != null)
        {
            runSoulsText.text = runSouls.ToString();
        }
    }
}