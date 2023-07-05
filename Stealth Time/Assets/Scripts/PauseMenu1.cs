using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private CursorLockMode previousCursorLockMode;
    private bool previousCursorVisibility;
    private float previousTimeScale;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        previousCursorLockMode = Cursor.lockState;
        previousCursorVisibility = Cursor.visible;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
        pauseMenuUI.SetActive(true); // Show pause menu UI
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f; // Freezes the game
        Debug.Log("Game paused");
    }

    void ResumeGame()
    {
        isPaused = false;
        Cursor.lockState = previousCursorLockMode; // Restore previous cursor lock mode
        Cursor.visible = previousCursorVisibility; // Restore previous cursor visibility
        pauseMenuUI.SetActive(false); // Hide pause menu UI
        Time.timeScale = previousTimeScale; // Resume the game
        Debug.Log("Game resumed");
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game before switching scenes
        SceneManager.LoadScene(0);
        Debug.Log("Quit to main menu");
    }

    public void Quit()
    {
        Time.timeScale = 1f; // Unfreeze the game before switching scenes
        Application.Quit();
        Debug.Log("Quit");
    }
}
