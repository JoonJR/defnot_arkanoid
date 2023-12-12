using UnityEngine;

public class ExtraBallsPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            BallsManager.Instance.SpawnExtraBalls(2);
            Destroy(gameObject); // Destroy the powerup after collection
        }
    }
}