using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Paddle : MonoBehaviour
{
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    public float moveSpeed = 10.0f;
    public LayerMask wallLayer; // Set this in the inspector to the layer your walls are on

    private Rigidbody2D _rigidbody2D;
    private float _paddleWidth;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _paddleWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        HandlePaddleMovement();
    }

    private void HandlePaddleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the starting point of the raycast to be on the edge of the paddle
        float raycastStartPointOffset = _paddleWidth * 0.5f; // Half the width of the paddle
        Vector2 raycastStartPoint = (Vector2)transform.position + new Vector2(horizontalInput * raycastStartPointOffset, 0);

        // Determine the direction of the raycast based on the input
        Vector2 direction = horizontalInput < 0 ? Vector2.left : Vector2.right;

        // Cast the ray to detect walls
        // RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction, Mathf.Abs(horizontalInput) * raycastStartPointOffset, wallLayer);
        // if (hit.collider != null)
        // {
        //     // If a wall is detected, prevent further movement in that direction
        //     horizontalInput = 0;
        // }

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