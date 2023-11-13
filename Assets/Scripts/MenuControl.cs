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
  
    public void Quit()
    {
        Application.Quit();
    }
}

 
