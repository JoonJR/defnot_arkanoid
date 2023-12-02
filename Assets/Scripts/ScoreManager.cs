using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    public TextMeshProUGUI scoreText;
    public int lives = 3;
    public int score = 0;
    
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
    public void NegateLife(int life){
        lives = Mathf.Max(lives - life, 0);
        LivesDisplay.Instance.SetLives(lives);    }
    
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    public void ApplyScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }
    
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
        if (scene.name == "MainMenu" && scoreText != null) {
            scoreText.enabled = false;
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
