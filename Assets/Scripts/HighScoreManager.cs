using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;
    public string difficulty;
}

[System.Serializable]
public class HighScoresWrapper
{
    public HighScoreEntry[] highScores;
}

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager _instance;
    public static HighScoreManager Instance => _instance;

    private HighScoreEntry[] highScores = new HighScoreEntry[5];

    private void Awake()
    {
        if (_instance == null)
        {
            
            DontDestroyOnLoad(gameObject);
            _instance = this;
            LoadHighScores();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HiScores")
        {
            UpdateHighScoreUI();
        }
    }

    public void AddHighScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        string difficulty = GameManager.CurrentDifficulty.ToString();

        HighScoreEntry newEntry = new HighScoreEntry { playerName = playerName, score = score, difficulty = difficulty };

        List<HighScoreEntry> highScoreList = highScores.Where(h => h != null).ToList();
        highScoreList.Add(newEntry);
        highScoreList = highScoreList.OrderByDescending(h => h.score).ToList();

        if (highScoreList.Count > 5)
        {
            highScoreList.RemoveRange(5, highScoreList.Count - 5);
        }

        highScores = highScoreList.ToArray();

        Debug.Log("New high score added. Current high scores:");
        foreach (var entry in highScores)
        {
            Debug.Log($"{entry.playerName} - {entry.score} ({entry.difficulty})");
        }

        SaveHighScores();
       
    }

    private void UpdateHighScoreUI()
    {
        TextMeshProUGUI[] highScoreTexts = GameObject.FindGameObjectsWithTag("HighScoreText")
                                                 .Select(obj => obj.GetComponent<TextMeshProUGUI>())
                                                 .ToArray();

        for (int i = 0; i < highScores.Length; i++)
        {
            if (i < highScoreTexts.Length)
            {
                if (highScores[i] != null)
                {
                    highScoreTexts[i].text = $"{i + 1}. {highScores[i].playerName}  {highScores[i].score} ({highScores[i].difficulty})";
                }
                else
                {
                    highScoreTexts[i].text = $"{i + 1}. ---";
                }
            }
        }
    }

    private void SaveHighScores()
    {
        HighScoresWrapper wrapper = new HighScoresWrapper { highScores = highScores };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("HighScores", json);
        Debug.Log($"Saved High Scores: {json}");
    }

    private void LoadHighScores()
    {
        string jsonString = PlayerPrefs.GetString("HighScores", "{}");
        HighScoresWrapper wrapper = JsonUtility.FromJson<HighScoresWrapper>(jsonString);
        highScores = wrapper.highScores ?? new HighScoreEntry[5];
        Debug.Log($"Loaded High Scores: {jsonString}");
    }
}