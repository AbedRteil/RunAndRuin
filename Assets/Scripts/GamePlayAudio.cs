using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameplayMusic();
        }
    }
}