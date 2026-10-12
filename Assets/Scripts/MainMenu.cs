using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameplayMusic();
        }

        SceneManager.LoadScene("UpdatedGame");
    }
}