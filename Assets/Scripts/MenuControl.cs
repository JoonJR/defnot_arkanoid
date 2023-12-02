using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        PauseMenu.Instance.GameIsPaused = false;
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
  
    public void Quit()
    {
        Application.Quit();
    }
  
}

 
