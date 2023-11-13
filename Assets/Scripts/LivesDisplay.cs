using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesDisplay : MonoBehaviour
{
    private static LivesDisplay _instance;
    public static LivesDisplay Instance => _instance;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    

    void Start()
    {
        UpdateHeartDisplay();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetHeartsActive(false);
        }
        else
        {
            SetHeartsActive(true);
        }
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            UpdateHeartDisplay();
           
            

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
    private void SetHeartsActive(bool isActive)
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(isActive);
        }
    }

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

    // Call this method when lives change
    public void SetLives(int newLives)
    {
        ScoreManager.Instance.lives = newLives;
        UpdateHeartDisplay();
    }
}