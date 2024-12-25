using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the Pause Menu UI

    // Method to Resume the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume the game
    }

    // Method to go back to the Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time is running
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    // Method to Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit(); // Quit the application
    }
}
