using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        // Check for Enter key or mouse click to start the game
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Ensure game runs at normal time
        SceneManager.LoadScene(1); // Load the gameplay scene
    }
}
