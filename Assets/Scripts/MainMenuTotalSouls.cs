using UnityEngine;
using TMPro;

public class MainMenuSouls : MonoBehaviour
{
    public TextMeshProUGUI totalSoulsText;

    void Start()
    {
        int totalSouls = PlayerPrefs.GetInt("TotalSouls", 0);
        totalSoulsText.text = totalSouls.ToString();
    }
}