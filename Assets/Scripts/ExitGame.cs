using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Attach this method to your UI Button's OnClick() event
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
