using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;

    [Header("Music Button")]
    public Image musicButtonImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    private const string MusicKey = "MusicEnabled";

    void Awake()
    {
        // Keep only one AudioManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved music setting
        bool musicEnabled = PlayerPrefs.GetInt(MusicKey, 1) == 1;

        musicSource.mute = !musicEnabled;

        // Start with Main Menu music
        PlayMenuMusic();

        UpdateMusicButton();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

        PlayerPrefs.SetInt(MusicKey, musicSource.mute ? 0 : 1);
        PlayerPrefs.Save();

        UpdateMusicButton();
    }

    public void PlayMenuMusic()
    {
        if (musicSource.clip == menuMusic && musicSource.isPlaying)
            return;

        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameplayMusic()
    {
        if (musicSource.clip == gameplayMusic && musicSource.isPlaying)
            return;

        musicSource.clip = gameplayMusic;
        musicSource.Play();
    }

    void UpdateMusicButton()
    {
        if (musicButtonImage == null)
            return;

        if (musicSource.mute)
            musicButtonImage.sprite = musicOffSprite;
        else
            musicButtonImage.sprite = musicOnSprite;
    }

    public bool IsMusicEnabled()
    {
        return !musicSource.mute;
    }
    public void StopMusic()
{
    musicSource.Stop();
}
}