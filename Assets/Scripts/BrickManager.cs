using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    private static BrickManager _instance;
    public static BrickManager Instance => _instance;

    // A collection to keep track of all brick instances in the game
    private HashSet<Bricks> allBricks = new HashSet<Bricks>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Method to register a new brick
    public void RegisterBrick(Bricks brick)
    {
        allBricks.Add(brick);
    }
    // Method to unregister a brick, typically when it's destroyed
    public void UnregisterBrick(Bricks brick)
    {
        allBricks.Remove(brick);
    }
    // Method to check if all bricks have been destroyed
    public bool AreAllBricksDestroyed()
    {
        return allBricks.Count == 0;
    }
}

