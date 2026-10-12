
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MonoBehaviour
{
    public CharacterType characterToSelect;
    public string gameSceneName = "Game";

    public GameObject comingSoonPopup;

    public void SelectAndPlay()
    {
        if (characterToSelect == CharacterType.Knight)
{
    comingSoonPopup.SetActive(true);
    Debug.Log("Knight - Coming Soon!");

    Invoke(nameof(HideComingSoon), 1.5f);
    return;
}

        GameData.SelectedCharacter = characterToSelect;
        Debug.Log("Selected character: " + characterToSelect);
        SceneManager.LoadScene(gameSceneName);
    }
    void HideComingSoon()
{
    comingSoonPopup.SetActive(false);
}
}

