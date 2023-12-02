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
        switch (powerUpType)
        {
            case PowerUpType.Enlarge:
                Debug.Log("Enlarge");
                Paddle.Instance.ChangePaddleSize(+0.5f); // Enlarge
                yield return new WaitForSeconds(duration);
                Paddle.Instance.ChangePaddleSize(-0.5f); // Revert to original size
                break;
            case PowerUpType.Shrink:
                Debug.Log("Shrink");
                Paddle.Instance.ChangePaddleSize(-0.5f); // Shrink
                yield return new WaitForSeconds(duration);
                Paddle.Instance.ChangePaddleSize(+0.5f); // Revert to original size
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
                foreach (Ball ball in BallsManager.Instance.Balls)
                {
                    // Apply the power-up effect to each ball
                    ball.SetSpeed(5f);
                }
                yield return new WaitForSeconds(duration);
                foreach (Ball ball in BallsManager.Instance.Balls)
                {
                    // Apply the power-up effect to each ball
                    ball.SetSpeed(10f);
                }
                break;
            case PowerUpType.FastBall:
                Debug.Log("FastBall");
                foreach (Ball ball in BallsManager.Instance.Balls)
                {
                    // Apply the power-up effect to each ball
                    ball.SetSpeed(13f);
                }
                yield return new WaitForSeconds(duration);
                foreach (Ball ball in BallsManager.Instance.Balls)
                {
                    // Apply the power-up effect to each ball
                    ball.SetSpeed(10f);
                }
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

