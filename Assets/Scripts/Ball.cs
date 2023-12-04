using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float constantSpeed = 10f;
    private void Start()
    {
        AdjustSpeedBasedOnDifficulty();
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void AdjustSpeedBasedOnDifficulty()
    {
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
        SetSpeed(constantSpeed * difficultyMultiplier);
    }
    // Used by PowerUps
    public void SetSpeed(float newSpeed)
    {
        constantSpeed = newSpeed;
    }
    private void FixedUpdate()
    {
        // Maintain constant speed
        _rigidbody.velocity = _rigidbody.velocity.normalized * constantSpeed;
    }

    public void DestroyBall(){
        if (gameObject != null) {
            Destroy(gameObject);
            BallsManager.Instance.RemoveBall(this);
        }
 
    }
}