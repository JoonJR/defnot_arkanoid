using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void ChangeSpeed(float speedMultiplier)
    {
        _rigidbody.velocity *= speedMultiplier;
    }
    public void DestroyBall(){
        if (gameObject != null) {
            Destroy(gameObject);
            BallsManager.Instance.RemoveBall(this);
        }
 
    }
}