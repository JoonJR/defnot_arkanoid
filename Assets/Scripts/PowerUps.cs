using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Enlarge, Shrink, ExtraLife, SlowBall, FastBall, ExtraBalls }
    public PowerUpType powerUpType;
    public AudioClip PowerUpClip;
   
    public float duration = 4f; // Duration of the power-up effect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            AudioManager.Instance.PlayEffect(PowerUpClip);
            StartCoroutine(ApplyPowerUp());
            // Toggle collider and renderer off, because object needs to stay active till coroutine is finished
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
             
        }
        // If PowerUp collides with DeathWall, destroy. 
        if (collision.gameObject.CompareTag("DeathWall"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyPowerUp()
    {
        float originalSpeed = BallsManager.Instance.GetOriginalBallSpeed(); // Get the base speed before power-up
        switch (powerUpType)
        {
            case PowerUpType.Enlarge:
                Debug.Log("Enlarge");
                Paddle.Instance.TemporarilyChangePaddleSize(1.2f, duration); // Temporarily enlarge
                break;

            case PowerUpType.Shrink:
                Debug.Log("Shrink");
                Paddle.Instance.TemporarilyChangePaddleSize(0.8f, duration); // Temporarily enlarge
                break;

            case PowerUpType.ExtraLife:
                if (ScoreManager.Instance.lives != 6) // if not max lives
                {
                    Debug.Log("ExtraLife");
                    ScoreManager.Instance.NegateLife(-1); // Increase life
                }
                break;

            case PowerUpType.SlowBall:
                Debug.Log("SlowBall");
                Debug.Log("SlowBall");
                BallsManager.Instance.ChangeBallSpeed(originalSpeed * 0.5f); // Slow down to 50% of original speed
                yield return new WaitForSeconds(duration);
                BallsManager.Instance.ChangeBallSpeed(originalSpeed); // Revert to original speed
                break;

            case PowerUpType.FastBall:
                Debug.Log("FastBall");
                BallsManager.Instance.ChangeBallSpeed(originalSpeed * 1.5f); // Speed up to 150% of original speed
                yield return new WaitForSeconds(duration);
                BallsManager.Instance.ChangeBallSpeed(originalSpeed); // Revert to original speed
                break;

            case PowerUpType.ExtraBalls:
                Debug.Log("ExtraBalls");
                BallsManager.Instance.SpawnExtraBalls(2); // Spawn two extra balls
                break;
        }
        // Destroy gameObject when coroutine finished. 
        Destroy(gameObject);
    }
    
}

