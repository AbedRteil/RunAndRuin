using UnityEngine;
using TMPro;

public class MainMenuHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.text = "BEST: " + highScore;
    }
}