using System.Collections;
using System.Collections.Generic;
using UnityEngine.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public AudioClip normalHitSound;
    private int silverBrickHealth = 2;
    private int iceBrickHealth = 3;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        BrickManager.Instance.RegisterBrick(this);
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level3" || SceneManager.GetActiveScene().name == "Level4")
        {
            animator = GetComponent<Animator>();
        }
    }

    private void TrySpawnPowerUp()
    {
        if (Random.Range(0, 5) == 0) // 1 in 5 chance to get PowerUp
        {
            SpawnPowerUp();
        }
    }
    private void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length > 0)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length); // Spawn random PowerUp
            Instantiate(powerUpPrefabs[powerUpIndex], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("PowerUp prefabs array is empty!");
        }
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
                AudioManager.Instance.PlayEffect(normalHitSound);
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(10);
            }
            else if(this.gameObject.tag == "GreenBrick") // 20 points
            {
                AudioManager.Instance.PlayEffect(normalHitSound);
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(20);
            }
            else if (this.gameObject.tag == "BlueBrick") // 30 points
            {
                AudioManager.Instance.PlayEffect(normalHitSound);
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(30);
            }
            else if (this.gameObject.tag == "RedBrick") // 40 points
            {
                AudioManager.Instance.PlayEffect(normalHitSound);
                Destroy(gameObject);
                TrySpawnPowerUp();
                ScoreManager.Instance.ApplyScore(40);
            }
            else if(this.gameObject.tag == "SilverBrick") // 50 points
            {
                animator.SetTrigger("silverTrigger");
                AudioManager.Instance.PlayEffect(normalHitSound);
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
                animator.SetTrigger("iceTrigger");
                AudioManager.Instance.PlayEffect(normalHitSound);
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
