using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Enlarge, Shrink, ExtraLife, SlowBall, FastBall }
    public PowerUpType powerUpType;
    public float duration = 10f; // Duration of the power-up effect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            StartCoroutine(ApplyPowerUp());
            Destroy(gameObject); // Destroy the power-up after applying its effect
        }
    }

    private IEnumerator ApplyPowerUp()
    {
        switch (powerUpType)
        {
            case PowerUpType.Enlarge:
                Debug.Log("Enlarge");
                Paddle.Instance.ChangePaddleSize(1.5f); // Enlarge
                yield return new WaitForSeconds(duration);
                Paddle.Instance.ChangePaddleSize(1.0f); // Revert to original size
                break;
            case PowerUpType.Shrink:
                Debug.Log("Shrink");
                Paddle.Instance.ChangePaddleSize(0.5f); // Shrink
                yield return new WaitForSeconds(duration);
                Paddle.Instance.ChangePaddleSize(1.0f); // Revert to original size
                break;
            case PowerUpType.ExtraLife:
                Debug.Log("ExtraLife");
                ScoreManager.Instance.NegateLife(-1); // Increase life
                break;
            case PowerUpType.SlowBall:
                Debug.Log("SlowBall");
                BallsManager.Instance.ChangeBallSpeed(0.5f); // Slow down
                yield return new WaitForSeconds(duration);
                BallsManager.Instance.ChangeBallSpeed(1.0f); // Revert to original speed
                break;
            case PowerUpType.FastBall:
                Debug.Log("FastBall");
                BallsManager.Instance.ChangeBallSpeed(1.5f); // Speed up
                yield return new WaitForSeconds(duration);
                BallsManager.Instance.ChangeBallSpeed(1.0f); // Revert to original speed
                break;
        }
    }
}

