using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField]
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if(ball != null){
            ball.DestroyBall();
            Debug.Log("Lol");
            
            ScoreManager.Instance.NegateLife(1);
            // ScoreManager.Instance.NegateLife(1);
            
        }
        
        

    }

    


    

}
