using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesDisplay : MonoBehaviour
{
    private static LivesDisplay _instance;
    public static LivesDisplay Instance => _instance;

    public Image[] hearts; // Array to hold heart images
    public Sprite fullHeart;  // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    void Start()
    {
        UpdateHeartDisplay(); // Update the heart display on start
    }
    private void Update()
    {
        // Check if the current scene requires displaying hearts
        GameObject hearthPanel = GameObject.FindWithTag("HearthPanel");
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Settings" || SceneManager.GetActiveScene().name == "Difficulty" || SceneManager.GetActiveScene().name == "Nickname" || SceneManager.GetActiveScene().name == "HiScores")
        {
            SetHeartsActive(false); // Hide hearts in these scenes
        }
        else
        {
            SetHeartsActive(true); // Show hearts in game scenes

        }
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            UpdateHeartDisplay(); // Update hearts when Level 1 is loaded
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Method to show or hide heart images
    private void SetHeartsActive(bool isActive)
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(isActive);
        }
    }
    // Method to update the heart display based on the player's lives
    public void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < ScoreManager.Instance.lives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    // Method to set the number of lives and update the display
    public void SetLives(int newLives)
    {
        ScoreManager.Instance.lives = newLives;
        UpdateHeartDisplay();
    }
}