using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour
{
    private int silverBrickHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        BrickManager.Instance.RegisterBrick(this);
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public bool AreAllBricksDestroyed()
    {
        int brickLayer = LayerMask.NameToLayer("BrickLayer");
        int layerMask = 1 << brickLayer;

        // Check a large enough area that encompasses your game area
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, 100f, layerMask);

        return colliders.Length == 0; // If no colliders are found, all bricks are destroyed
    }
    private void OnDestroy()
    {
        BrickManager.Instance.UnregisterBrick(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (this.gameObject.tag == "YellowBrick") // 10 points
            {
                Destroy(gameObject);
                ScoreManager.Instance.ApplyScore(10);
            }
            else if(this.gameObject.tag == "GreenBrick") // 20 points
            {
                Destroy(gameObject);
                ScoreManager.Instance.ApplyScore(20);
            }
            else if (this.gameObject.tag == "BlueBrick") // 30 points
            {
                Destroy(gameObject);
                ScoreManager.Instance.ApplyScore(30);
            }
            else if (this.gameObject.tag == "RedBrick") // 40 points
            {
                Destroy(gameObject);
                ScoreManager.Instance.ApplyScore(40);
            }
            else if(this.gameObject.tag == "SilverBrick") // 50 points
            {
                silverBrickHealth -= 1;

                if (silverBrickHealth == 0)
                {
                    Destroy(gameObject); 
                    ScoreManager.Instance.ApplyScore(50); 
                }
                    
            }
        }
    }
}
