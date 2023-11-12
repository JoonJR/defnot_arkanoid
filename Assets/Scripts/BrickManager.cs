using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    private static BrickManager _instance;
    public static BrickManager Instance => _instance;

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
    public void RegisterBrick(Bricks brick)
    {
        allBricks.Add(brick);
    }

    public void UnregisterBrick(Bricks brick)
    {
        allBricks.Remove(brick);
    }

    public bool AreAllBricksDestroyed()
    {
        return allBricks.Count == 0;
    }
}

