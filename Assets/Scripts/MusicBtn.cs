using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public Image buttonImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    void Start()
    {
        UpdateImage();
    }

    public void ToggleMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic();
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        if (AudioManager.Instance == null || buttonImage == null)
            return;

        if (AudioManager.Instance.IsMusicEnabled())
            buttonImage.sprite = musicOnSprite;
        else
            buttonImage.sprite = musicOffSprite;
    }
}