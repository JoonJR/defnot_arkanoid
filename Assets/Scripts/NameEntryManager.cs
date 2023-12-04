using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NameEntryManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;

    private void Start()
    {
        submitButton.onClick.AddListener(OnSubmitName);
    }

    public void OnSubmitName()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            if (playerName.Length  != 10)
            {
                int spacesNeeded = 10 - playerName.Length;

                string spaces = new string(' ', 2*spacesNeeded);
                playerName = playerName + spaces; 
            }
            // Save the player's name for later use
            PlayerPrefs.SetString("PlayerName", playerName);

            // Load the next scene (e.g., the main game scene)

            SceneManager.LoadScene("Level1");
            if (PauseMenu.Instance != null)
            {
                PauseMenu.Instance.GameIsPaused = false;
            }
        }
        else
        {
            // Optionally, prompt the user that the name cannot be empty
            Debug.Log("Name cannot be empty.");
        }
    }
}