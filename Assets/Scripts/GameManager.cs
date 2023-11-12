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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("PauseMenu");
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        if (currentSceneName == "Level1" || currentSceneName == "Level2" || currentSceneName == "Level3")
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
            IsGameStarted = false;
            if (SceneManager.GetActiveScene().name != "Level3")
            {
                i++;
                currentLevel = "Level" + i;
                Debug.Log(i);
                SceneManager.LoadScene("Level" + i);
            }
            
        }
    }

}
