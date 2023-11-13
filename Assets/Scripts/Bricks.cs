using System.Collections;
using System.Collections.Generic;
using UnityEngine.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    private int silverBrickHealth = 3;
    private int iceBrickHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        BrickManager.Instance.RegisterBrick(this);
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void TrySpawnPowerUp()
    {
        if (Random.Range(0, 0) == 0) // 1 in 5 chance to get PowerUp
        {
            SpawnPowerUp();
        }
    }
    private void SpawnPowerUp()
    {
        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length); // Spawn random PowerUp
        Instantiate(powerUpPrefabs[powerUpIndex], transform.position, Quaternion.identity);
    }
    private void OnDestroy()
    {
        BrickManager.Instance.UnregisterBrick(this);
    }
    private void OnCollisionEnter2D(Collision2D collision) // Handle destroying bricks and apply score. 
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (this.gameObject.tag == "YellowBrick") // 10 points
            {
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(10);
            }
            else if(this.gameObject.tag == "GreenBrick") // 20 points
            {
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(20);
            }
            else if (this.gameObject.tag == "BlueBrick") // 30 points
            {
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(30);
            }
            else if (this.gameObject.tag == "RedBrick") // 40 points
            {
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(40);
            }
            else if(this.gameObject.tag == "SilverBrick") // 50 points
            {
                silverBrickHealth -= 1;

                if (silverBrickHealth == 0)
                {
                    Destroy(gameObject);
                    TrySpawnPowerUp();
                    ScoreManager.Instance.ApplyScore(50); 
                }    
            }
            else if (this.gameObject.tag == "IceBrick") // 100 points
            {
                iceBrickHealth -= 1;
                if (iceBrickHealth == 0)
                {
                    Destroy(gameObject);
                    TrySpawnPowerUp();
                    ScoreManager.Instance.ApplyScore(100);
                }
            }
        }

    }
}
