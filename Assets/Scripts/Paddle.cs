using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    public AudioClip paddleHit;               // Sound effect for paddle hit.
    public float moveSpeed = 10.0f;          // Normal speed at which the paddle moves.
    private Vector3 originalScale;          // Original scale of the paddle.
    private Coroutine sizeChangeCoroutine; // Coroutine for changing the paddle size temporarily.

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            originalScale = transform.localScale;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Adjust the speed and size of the paddle based on the game difficulty.
        AdjustSpeedBasedOnDifficulty();
        AdjustPaddleBaseSize();
    }

    void Update()
    {
        // Handles the movement of the paddle.
        HandlePaddleMovement();
    }

    // Adjusts the paddle's speed based on the game's difficulty level.
    void AdjustSpeedBasedOnDifficulty()
    {
        switch (GameManager.CurrentDifficulty)
        {
            case DifficultyLevel.Easy:
                moveSpeed *= 0.8f;
                break;
            case DifficultyLevel.Normal:
                moveSpeed *= 1f;
                break;
            case DifficultyLevel.Hard:
                moveSpeed *= 1.2f;
                break;
        }
    }
    // Adjusts the base size of the paddle based on the game's difficulty level.
    void AdjustPaddleBaseSize()
    {
        float sizeMultiplier = GetSizeMultiplierBasedOnDifficulty();
        transform.localScale = new Vector3(originalScale.x * sizeMultiplier, originalScale.y, originalScale.z);
        Debug.Log($"[Paddle] Base Paddle Size Set: {transform.localScale}");
    }

    // Returns the size multiplier based on the game's difficulty level.
    private float GetSizeMultiplierBasedOnDifficulty()
    {
        switch (GameManager.CurrentDifficulty)
        {
            case DifficultyLevel.Easy:
                return 1.2f;
            case DifficultyLevel.Normal:
                return 1f;
            case DifficultyLevel.Hard:
                return 0.8f;
            default:
                return 1f;
        }
    }

    // Temporarily changes the size of the paddle.
    public void TemporarilyChangePaddleSize(float temporaryMultiplier, float duration)
    {
        if (sizeChangeCoroutine != null)
        {
            StopCoroutine(sizeChangeCoroutine);
        }
        sizeChangeCoroutine = StartCoroutine(ChangePaddleSizeTemporarily(temporaryMultiplier, duration));
    }

    // Coroutine to change the paddle size temporarily.
    private IEnumerator ChangePaddleSizeTemporarily(float temporaryMultiplier, float duration)
    {
        Vector3 currentSize = transform.localScale;
        Vector3 newSize = new Vector3(currentSize.x * temporaryMultiplier, currentSize.y, currentSize.z);
        transform.localScale = newSize;
        Debug.Log($"[Paddle] Temporary Paddle Size: {newSize}");

        yield return new WaitForSeconds(duration);

        transform.localScale = currentSize; // Revert to current size before temporary change
        Debug.Log($"[Paddle] Paddle Size Reverted to: {currentSize}");
    }

    // Handles the movement of the paddle based on player input.
    private void HandlePaddleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        _rigidbody2D.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the paddle hits the ball.
        if (collision.gameObject.tag == "Ball" & BallsManager.Instance.isInPlay)
        {
            // Play paddle hit sound effect.
            AudioManager.Instance.PlayEffect(paddleHit);

            // Calculate the direction to send the ball based on where it hits the paddle.
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;
            
            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
        }
    }
}
