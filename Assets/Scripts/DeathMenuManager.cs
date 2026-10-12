using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenuManager : MonoBehaviour
{
    public static DeathMenuManager Instance;

public GameObject Souls;
    public GameObject deathPanel;
    public GameObject GamePlayUI; // Reference to the gameplay UI panel
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI runSoulsDisplayText;
    public TextMeshProUGUI totalSoulsDisplayText;

    void Awake()
    {
        Instance = this;
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }

    public void ShowDeathMenu(int finalScore)
{
    int bestScore = PlayerPrefs.GetInt("HighScore", 0);

    if (finalScoreText != null)
    {
        finalScoreText.text = "SCORE: " + finalScore;
    }

    if (bestScoreText != null)
    {
        bestScoreText.text = "HIGHSCORE: " + bestScore;
    }

    if (SoulManager.Instance != null)
    {
        SoulManager.Instance.BankRunSouls();

        if (runSoulsDisplayText != null)
        {
            runSoulsDisplayText.text = "SOULS: " + SoulManager.Instance.GetRunSouls();
        }

        if (totalSoulsDisplayText != null)
        {
            totalSoulsDisplayText.text = "TOTAL SOULS: " + SoulManager.Instance.GetTotalSouls();
        }
    }

    if (deathPanel != null)
    {
        deathPanel.SetActive(true);
    }
    if (GamePlayUI != null)
{
    GamePlayUI.SetActive(false);
    Souls.SetActive(false); // Hide the Souls UI when the death menu is shown
}

    Time.timeScale = 0f;
}

    // Hook this to the Restart button's OnClick in the Inspector
    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Hook this to the Main Menu button's OnClick in the Inspector
    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}