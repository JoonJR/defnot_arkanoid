using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    public AudioClip loseLifeClip;
    private void OnCollisionEnter2D(Collision2D collision){
        
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if(ball != null){
            AudioManager.Instance.PlayEffect(loseLifeClip);
            ball.DestroyBall();
            BallsManager.Instance.isInPlay = false;

        }
    }
    
}
