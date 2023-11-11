using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private static PauseMenu _instance;
    public static PauseMenu Instance => _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
        // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    


    
    public void LoadMainMenu() {

        Resume();
        if (Paddle.Instance != null)
        {
            Destroy(Paddle.Instance.gameObject);
        }
        GameManager.manager.IsGameStarted = false;
        GameManager.manager.currentLevel = "Level" + 1;
        GameManager.manager.i = 1;
        Time.timeScale = 1f;
        ScoreManager.Instance.score = 0;
        ScoreManager.Instance.lives = 3;
        
        SceneManager.LoadScene("MainMenu");
    }
   
}
