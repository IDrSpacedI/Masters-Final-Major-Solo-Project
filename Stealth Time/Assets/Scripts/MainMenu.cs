using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the next scene in the build settings (assuming it's the game scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel1()
    {
        // Restart level 1 by reloading the scene
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        // Exit the application (works in standalone builds)
        Application.Quit();
    }
}
