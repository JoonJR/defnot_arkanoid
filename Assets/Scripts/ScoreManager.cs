using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    public TextMeshProUGUI scoreText; // Text UI for displaying the score
    public int lives = 3;  // Starting lives
    public int score = 0; // Starting score
    
    private void Awake(){

        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Method to decrease lives and update UI
    public void NegateLife(int life){
        lives = Mathf.Max(lives - life, 0);     // Ensure lives don't go below zero
        LivesDisplay.Instance.SetLives(lives); // Update the lives display   
    }
    // Method to update the score UI
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    // Method to add points to the score
    public void ApplyScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }
    // Method to find and assign the scoreText UI element
    public void FindUIElements()
    {
        GameObject scoreTextObj = GameObject.FindGameObjectWithTag("ScoreText");
        if (scoreTextObj != null)
        {
            scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Score Text object not found");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Handling UI elements based on the scene
        if (scene.name == "MainMenu" && scoreText != null) {
            scoreText.enabled = false; // Hide score text in the main menu
        }
        else if(scene.name == "Level1")
        {
            FindUIElements();
            UpdateScoreUI();
        }
        else if (scene.name == "Level2")
        {
            FindUIElements();
            UpdateScoreUI();
        }
        else if (scene.name == "Level3")
        {
            FindUIElements();
            UpdateScoreUI();
        }
        else if (scene.name == "Level4")
        {
            FindUIElements();
            UpdateScoreUI();
        }

    }
}
