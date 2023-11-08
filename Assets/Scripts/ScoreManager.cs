using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

   
    public TextMeshProUGUI livesText;
    private int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: 3";
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
    }
    public void NegateLife(int life){
        lives = Mathf.Max(lives - life, 0);
        UpdateLivesUI();

    }
    private void UpdateLivesUI(){
        livesText.text = "Lives: " + lives;
    }
}
