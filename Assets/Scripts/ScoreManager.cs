using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public PlayerMovement player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreToBeatText;

    private float score;
    private int highScore;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        scoreToBeatText.text = "SCORE TO BEAT: " + highScore;
    }

    void Update()
    {
        if (player.gameStarted)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }

        int currentScore = Mathf.FloorToInt(score);

        scoreText.text = "SCORE: " + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;

            scoreToBeatText.text = "SCORE TO BEAT: " + highScore;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // DEV ONLY: Press R to reset high score
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("HighScore");
            PlayerPrefs.Save();

            highScore = 0;
            scoreToBeatText.text = "SCORE TO BEAT: 0";

            Debug.Log("High Score Reset!");
        }
    }

    public int GetCurrentScore()
    {
        return Mathf.FloorToInt(score);
    }
}