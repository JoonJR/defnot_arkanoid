using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Paddle : MonoBehaviour
{
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    public float moveSpeed = 10.0f;
    public float paddleWidth = 1.5f; 
    public LayerMask wallLayer; 

    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        


    }

    void Update()
    {
        HandlePaddleMovement();
    }

    private void HandlePaddleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // Apply the movement
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
    public void ChangePaddleSize(float sizeMultiplier)
    {
        Vector3 scale = transform.localScale;
        scale.x *= sizeMultiplier;
        transform.localScale = scale;
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
}