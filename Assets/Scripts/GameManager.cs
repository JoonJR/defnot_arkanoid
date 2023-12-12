using UnityEngine;
using UnityEngine.SceneManagement;

// Different difficulty levels
public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard
}

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    
    public static DifficultyLevel CurrentDifficulty { get; set; } = DifficultyLevel.Normal; // Default difficulty

    public bool IsGameStarted { get; set; }
    public string currentLevel = "Level" + 1;
    public int i = 1;

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // if we are on level scene, check the completion.
        if (currentSceneName == "Level1" || currentSceneName == "Level2" || currentSceneName == "Level3" || currentSceneName == "Level4")
        {
            CheckLevelCompletion();
        }
    }
    
    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void SetDifficulty(DifficultyLevel difficulty)
    {
        CurrentDifficulty = difficulty;
    }
    // Check if current level is completed
    void CheckLevelCompletion()
    {
        if (BrickManager.Instance.AreAllBricksDestroyed())
        {
            BallsManager.Instance.DestroyAllBalls();
            IsGameStarted = false;
            BallsManager.Instance.isInPlay = false;
            if (SceneManager.GetActiveScene().name != "Level4") // Last level.
            {
                i++;
                currentLevel = "Level" + i;
                Debug.Log(i);
                SceneManager.LoadScene("Level" + i);
            }
            
        }
    }

}
