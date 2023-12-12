using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float constantSpeed = 10f; // Base speed of the ball
    public AudioClip wallHit; // Sound effect for wall collision

    private void Start()
    {
        AdjustSpeedBasedOnDifficulty(); // Adjust ball speed based on the selected difficulty level
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Adjust the ball's speed based on the game's difficulty level
    void AdjustSpeedBasedOnDifficulty()
    {
        float difficultyMultiplier = 1f; // Default multiplier for normal difficulty
        switch (GameManager.CurrentDifficulty)
        {
            case DifficultyLevel.Easy:
                difficultyMultiplier = 0.8f; // Slow down for easy difficulty
                break;
            case DifficultyLevel.Normal:
                difficultyMultiplier = 1f; // Standard speed for normal difficulty
                break;
            case DifficultyLevel.Hard:
                difficultyMultiplier = 1.2f; // Speed up for hard difficulty
                break;
        }
        SetSpeed(constantSpeed * difficultyMultiplier); // Apply the speed adjustment
    }
    // Method to set the ball's speed, used by PowerUps
    public void SetSpeed(float newSpeed)
    {
        constantSpeed = newSpeed;
    }
    private void FixedUpdate()
    {
        // Maintain constant speed
        _rigidbody.velocity = _rigidbody.velocity.normalized * constantSpeed;
    }
    // Method to destroy the ball
    public void DestroyBall(){
        if (gameObject != null) {
            Destroy(gameObject);
            BallsManager.Instance.RemoveBall(this); // Notify the BallsManager to remove this ball
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { // Play a sound effect when the ball hits a wall
        if (collision.gameObject.tag == "Wall")
        {
            AudioManager.Instance.PlayEffect(wallHit);
        }
        
    }
}

