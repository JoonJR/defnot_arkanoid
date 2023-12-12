using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        if (PauseMenu.Instance != null)
        {
            PauseMenu.Instance.GameIsPaused = false;
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PauseMenu.Instance.GameIsPaused=true;
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
        PauseMenu.Instance.GameIsPaused = true;
    }
    public void LoadDifficulty()
    {
        SceneManager.LoadScene("Difficulty");
    }
    public void LoadHiScores()
    {
        SceneManager.LoadScene("HiScores");
    }
    public void LoadNickname()
    {
        SceneManager.LoadScene("Nickname");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartGameWithDifficulty(string difficulty)
    {
        DifficultyLevel selectedDifficulty = DifficultyLevel.Normal; // Default

        switch (difficulty)
        {
            case "Easy":
                selectedDifficulty = DifficultyLevel.Easy;
                break;
            case "Normal":
                selectedDifficulty = DifficultyLevel.Normal;
                break;
            case "Hard":
                selectedDifficulty = DifficultyLevel.Hard;
                break;
        }

        // Assuming GameManager is accessible as a singleton
        if (GameManager.manager != null)
        {
            GameManager.CurrentDifficulty = selectedDifficulty;
            GameManager.manager.SetDifficulty(selectedDifficulty);
        }

        LoadNickname(); 
    }

}

 
