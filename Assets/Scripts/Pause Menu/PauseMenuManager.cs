using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the Pause Menu UI
    private bool isPaused = false; // Tracks if the game is paused or not

    public Button[] menuButtons; // Array of buttons in the pause menu
    private int currentButtonIndex = 0; // Tracks the currently selected button

    void Update()
    {
        // Toggle pause menu on/off when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // If game is paused, resume it
            }
            else
            {
                PauseGame(); // If game is running, pause it
            }
        }

        // If paused, handle navigation
        if (isPaused)
        {
            HandleButtonNavigation();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause the game
        HighlightButton(currentButtonIndex); // Highlight the default button
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // Hide pause menu
        Time.timeScale = 1f; // Resume the game
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time is running
        pauseMenu.SetActive(false); // Hide pause menu (if still active)
        isPaused = false; // Reset pause state
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode in editor
#endif
    }

    private void HandleButtonNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move up
            currentButtonIndex = (currentButtonIndex - 1 + menuButtons.Length) % menuButtons.Length;
            HighlightButton(currentButtonIndex);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move down
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
            HighlightButton(currentButtonIndex);
        }

        // Handle button selection with Enter or spacebar
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            menuButtons[currentButtonIndex].onClick.Invoke(); // Trigger the button action
        }
    }

    private void HighlightButton(int index)
    {
        // Reset all button colors
        foreach (var button in menuButtons)
        {
            button.GetComponent<Image>().color = Color.white; // Default color
        }

        // Highlight the selected button
        menuButtons[index].GetComponent<Image>().color = new Color(0.45f, 0.89f, 0f); // Highlight color (74E200)
    }

    // Add a method to handle mouse click selection
    public void OnMouseSelect(Button button)
    {
        // Find the index of the clicked button
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (menuButtons[i] == button)
            {
                currentButtonIndex = i; // Update the selected button index
                HighlightButton(currentButtonIndex); // Highlight the button
                break;
            }
        }
    }
}
