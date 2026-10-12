using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMenuMusic();
        }
    }
}