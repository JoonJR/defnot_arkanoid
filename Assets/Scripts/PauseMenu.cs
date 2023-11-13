using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public TextMeshProUGUI finalScoreText;

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
        // If we are in last level and all bricks are destroyed, game is over. 
        if(SceneManager.GetActiveScene().name == "Level3" && BrickManager.Instance.AreAllBricksDestroyed()) { 
            
            gameOverMenuUI.SetActive(true);
            GameIsPaused = true;
            pauseMenuUI.SetActive(false);
            UpdateFinalScoreUI();
            ScoreManager.Instance.livesText.enabled = false;
            ScoreManager.Instance.scoreText.enabled = false;
        }
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            
            ScoreManager.Instance.livesText.enabled = true;
            ScoreManager.Instance.scoreText.enabled = true;
            GameIsPaused = false;
        }
        // Disable 
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameOverMenuUI.SetActive(false);
            
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
    public void UpdateFinalScoreUI()
    {
        FindFinalScoreUI();
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + ScoreManager.Instance.score;
        }
    }
    private void FindFinalScoreUI()
    {
        GameObject finalScoreTextObj = GameObject.FindGameObjectWithTag("FinalScoreText");
        if (finalScoreTextObj != null)
        {
            finalScoreText = finalScoreTextObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Lives Text object not found");
        }
    }
    // When going to mainmenu reset everything.
    public void LoadMainMenu() { 
        
    Resume();
        
    if (Paddle.Instance != null)
    {
        Destroy(Paddle.Instance.gameObject);
    }
    pauseMenuUI.SetActive(false);
    BallsManager.Instance.DestroyAllBalls();
    ScoreManager.Instance.livesText.enabled = false;    
    ScoreManager.Instance.scoreText.enabled = false;
    GameManager.manager.IsGameStarted = false;
    GameManager.manager.currentLevel = "Level" + 1;
    GameManager.manager.i = 1;
    ScoreManager.Instance.score = 0;
    ScoreManager.Instance.lives = 3;
        
    SceneManager.LoadScene("MainMenu");
    }
   
}
