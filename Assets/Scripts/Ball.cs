using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float constantSpeed = 10f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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