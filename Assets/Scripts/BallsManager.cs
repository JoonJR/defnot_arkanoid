using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallsManager : MonoBehaviour
{
    private static BallsManager _instance;
    public static BallsManager Instance => _instance;

    [SerializeField]
    private Ball ballPrefab; // Prefab for creating new balls
    private Ball initialBall; // The initial ball in the game
    private Rigidbody2D initialBallRb; // Rigidbody of the initial ball
    public float initialBallSpeed = 250; // Speed at which the initial ball is launched
    public bool isInPlay = false; // Flag to check if the ball is in play
    public List<Ball> Balls { get; private set; } = new List<Ball>(); // List to keep track of all balls in play

    // Update is called once per frame
    void Update()
    {
        // Check if the game has not started and the Paddle instance is available
        if (!GameManager.manager.IsGameStarted && Paddle.Instance != null)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.45f, 0);
            if (initialBall != null) // Also check if the initialBall has been instantiated
            {
                initialBall.transform.position = ballPosition;
            }
            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) && !PauseMenu.Instance.GameIsPaused)
            {
                if (initialBall != null)
                {
                    isInPlay = true;
                    initialBallRb.isKinematic = false;
                    initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                    GameManager.manager.IsGameStarted = true;
                }  
            }
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Paddle.Instance != null)
        {
            InitBall();
        }
    }

    // Initialize the first ball of the game
    public void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .45f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        Balls.Clear();
        Balls.Add(initialBall);
    }

    // Change the speed of all balls in play
    public void ChangeBallSpeed(float newSpeed)
    {
        foreach (Ball ball in Balls)
        {
            ball.SetSpeed(newSpeed);
        }
    }

    // Get the original speed of the ball based on the difficulty level
    public float GetOriginalBallSpeed()
    {
        float baseSpeed = 10f; // Base speed
        float difficultyMultiplier = 1f;
        switch (GameManager.CurrentDifficulty)
        {
            case DifficultyLevel.Easy:
                difficultyMultiplier = 0.8f;
                break;
            case DifficultyLevel.Normal:
                difficultyMultiplier = 1f;
                break;
            case DifficultyLevel.Hard:
                difficultyMultiplier = 1.2f;
                break;
        }
        return baseSpeed * difficultyMultiplier;
    }

    // Remove a specific ball from the game
    public void RemoveBall(Ball ball)
    {
        Balls.Remove(ball);
        if (Balls.Count == 0)
        {
            // No balls left in the scene
            HandleNoBallsLeft();
        }
    }

    // Handle the situation when there are no balls left in the scene
    private void HandleNoBallsLeft()
    {
        // Deduct a life and spawn a new initial ball
        ScoreManager.Instance.NegateLife(1);
        GameManager.manager.IsGameStarted = false;
        InitBall();
    }
    public void DestroyAllBalls()
    {
        foreach (var ball in Balls)
        {
            if (ball != null) // Check if the ball is not already destroyed
            {
                Destroy(ball.gameObject); // Destroy the ball
            }
        }

        Balls.Clear(); // Clear the list after all balls are destroyed
    }

    // Spawn additional balls in the game
    public void SpawnExtraBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.45f, 0);
            Ball newBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
            Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(GetRandomDirection() * initialBallSpeed);
            Balls.Add(newBall);
        }
    }
    // Generate a random direction for the ball
    private Vector2 GetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(0.5f, 1f); // Ensuring the ball goes upwards
        return new Vector2(x, y).normalized; // Normalize to keep the speed consistent
    }

    
}
