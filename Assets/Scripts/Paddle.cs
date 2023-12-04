using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    public float moveSpeed = 10.0f;
    private Vector3 originalScale;
    private Coroutine sizeChangeCoroutine;

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
        AdjustSpeedBasedOnDifficulty();
        AdjustPaddleBaseSize();
    }

    void Update()
    {
        HandlePaddleMovement();
    }

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

    void AdjustPaddleBaseSize()
    {
        float sizeMultiplier = GetSizeMultiplierBasedOnDifficulty();
        transform.localScale = new Vector3(originalScale.x * sizeMultiplier, originalScale.y, originalScale.z);
        Debug.Log($"[Paddle] Base Paddle Size Set: {transform.localScale}");
    }

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

    public void TemporarilyChangePaddleSize(float temporaryMultiplier, float duration)
    {
        if (sizeChangeCoroutine != null)
        {
            StopCoroutine(sizeChangeCoroutine);
        }
        sizeChangeCoroutine = StartCoroutine(ChangePaddleSizeTemporarily(temporaryMultiplier, duration));
    }

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




    private void HandlePaddleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        _rigidbody2D.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
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
