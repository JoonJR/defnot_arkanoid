using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public bool IsGameStarted { get; set; }
    public string currentLevel = "Level" + 1;
    public int i = 1;

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
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
    void CheckLevelCompletion()
    {
        if (BrickManager.Instance.AreAllBricksDestroyed())
        {
            BallsManager.Instance.DestroyAllBalls();
            IsGameStarted = false;
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
