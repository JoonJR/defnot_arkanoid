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
         
            // Save the player's name for later use
            PlayerPrefs.SetString("PlayerName", playerName);

            // Load the next scene

            SceneManager.LoadScene("Level1");
            if (PauseMenu.Instance != null)
            {
                PauseMenu.Instance.GameIsPaused = false;
            }
        }
        else
        {
            Debug.Log("Name cannot be empty.");
        }
    }
}