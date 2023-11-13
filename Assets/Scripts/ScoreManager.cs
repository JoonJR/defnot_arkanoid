using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

   
    public TextMeshProUGUI livesText, scoreText;
    public int lives = 3;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        

    }
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
        LivesDisplay.Instance.SetLives(lives);
        UpdateLivesUI();
    }
    private void UpdateLivesUI(){
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
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
        GameObject livesTextObj = GameObject.FindGameObjectWithTag("LivesText");
        if (livesTextObj != null)
        {
            livesText = livesTextObj.GetComponent<TextMeshProUGUI>();
            
        }
        else
        {
            Debug.LogError("Lives Text object not found");
        }

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
        if (scene.name == "MainMenu" && livesText != null) {
            livesText.enabled = false;
            scoreText.enabled = false;
        }
        else if(scene.name == "Level1")
        {
            
            FindUIElements();
            UpdateLivesUI();
            UpdateScoreUI();
        }
        else if (scene.name == "Level2")
        {
            FindUIElements();
            UpdateLivesUI();
            UpdateScoreUI();
        }
        else if (scene.name == "Level3")
        {
            FindUIElements();
            UpdateLivesUI();
            UpdateScoreUI();
        }
        
    }
}
