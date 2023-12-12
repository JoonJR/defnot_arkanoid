using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Serializable class to store high score entries
[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;
    public string difficulty;
}

// Wrapper class to store an array of HighScoreEntries for serialization
[System.Serializable]
public class HighScoresWrapper
{
    public HighScoreEntry[] highScores;
}

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager _instance;
    public static HighScoreManager Instance => _instance;

    private HighScoreEntry[] highScores = new HighScoreEntry[5]; // Array to store top 5 high scores

    private void Awake()
    {
        
        if (_instance == null)
        {
            
            DontDestroyOnLoad(gameObject);
            _instance = this;
            LoadHighScores(); // Load high scores from PlayerPrefs
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
        // Update high score UI when the HiScores scene is loaded
        if (scene.name == "HiScores")
        {
            UpdateHighScoreUI();
        }
    }
    // Method to add a new high score
    public void AddHighScore(int score)
    {
        // Retrieve player name and difficulty from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        string difficulty = GameManager.CurrentDifficulty.ToString();

        // Create a new high score entry
        HighScoreEntry newEntry = new HighScoreEntry { playerName = playerName, score = score, difficulty = difficulty };

        // Add the new entry to the list and sort it
        List<HighScoreEntry> highScoreList = highScores.Where(h => h != null).ToList();
        highScoreList.Add(newEntry);
        highScoreList = highScoreList.OrderByDescending(h => h.score).ToList();

        // Keep only the top 5 scores
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

        SaveHighScores(); // Save the updated high scores

    }
    // Method to update the high score UI
    private void UpdateHighScoreUI()
    {
        // Find all high score name and value TextMeshProUGUI components
        TextMeshProUGUI[] nameTexts = GameObject.FindGameObjectsWithTag("HighScoreName")
                                                .OrderBy(go => go.name)
                                                .Select(go => go.GetComponent<TextMeshProUGUI>())
                                                .ToArray();
        TextMeshProUGUI[] scoreTexts = GameObject.FindGameObjectsWithTag("HighScoreValue")
                                                 .OrderBy(go => go.name)
                                                 .Select(go => go.GetComponent<TextMeshProUGUI>())
                                                 .ToArray();

        // Update the UI with high score data
        for (int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] != null)
            {
                if (i < nameTexts.Length)
                {
                    nameTexts[i].text = $"{i + 1}. {highScores[i].playerName}";
                }
                if (i < scoreTexts.Length)
                {
                    scoreTexts[i].text = $"{highScores[i].score} ({highScores[i].difficulty})";
                }
            }
            else
            {
                if (i < nameTexts.Length)
                {
                    nameTexts[i].text = $"{i + 1}. ----------";
                }
                if (i < scoreTexts.Length)
                {
                    scoreTexts[i].text = "---";
                }
            }
        }
    }

    // Method to save high scores to PlayerPrefs
    private void SaveHighScores()
    {
        HighScoresWrapper wrapper = new HighScoresWrapper { highScores = highScores };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("HighScores", json);
        Debug.Log($"Saved High Scores: {json}");
    }
    // Method to load high scores from PlayerPrefs
    private void LoadHighScores()
    {
        string jsonString = PlayerPrefs.GetString("HighScores", "{}");
        HighScoresWrapper wrapper = JsonUtility.FromJson<HighScoresWrapper>(jsonString);
        highScores = wrapper.highScores ?? new HighScoreEntry[5];
        Debug.Log($"Loaded High Scores: {jsonString}");
    }
}