using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;

    public void OpenCharacterSelect()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
     public void ExitGame()
    {
        // Closes the built application
        Application.Quit();

        // Closes the play mode inside the Unity Editor (for testing)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}