using UnityEngine;



public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void FreezeBall()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;
    }
    public void UnfreezeBall()
    {
        _rigidbody.isKinematic = false;
    }
    public void DestroyBall(){

        Destroy(gameObject);
        GameManager.manager.IsGameStarted = false;
        BallsManager.Instance.InitBall();

    }
}