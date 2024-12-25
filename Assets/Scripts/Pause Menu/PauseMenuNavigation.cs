using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuNavigation : MonoBehaviour
{
    public Button[] menuButtons; // Array of buttons in the pause menu
    private int selectedIndex = 0; // Index to track the currently selected button
    private Color normalColor = Color.white; // Default color of buttons
    private Color selectedColor = new Color(0.45f, 0.88f, 0.0f); // #74E200 color in RGB

    private void Start()
    {
        // Initialize the selected button (first button)
        SelectButton(selectedIndex);
    }

    private void Update()
    {
        // Get input for up and down arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Move up in the menu (decrease the index)
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = menuButtons.Length - 1; // Loop back to the last button
            }
            SelectButton(selectedIndex); // Update button selection
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Move down in the menu (increase the index)
            selectedIndex++;
            if (selectedIndex >= menuButtons.Length)
            {
                selectedIndex = 0; // Loop back to the first button
            }
            SelectButton(selectedIndex); // Update button selection
        }

        // Check if the Enter key is pressed to select the button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuButtons[selectedIndex].onClick.Invoke(); // Simulate button click
        }
    }

    private void SelectButton(int index)
    {
        // Reset color for all buttons and apply selected color to the current one
        foreach (Button button in menuButtons)
        {
            button.GetComponent<Image>().color = normalColor; // Reset color to normal
        }

        // Change color of the selected button
        menuButtons[index].GetComponent<Image>().color = selectedColor;
        menuButtons[index].Select(); // Select the button at the selected index
    }
}
