using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static PoolManager poolManager;

    public static GameManager Instance { get { return instance; } }
    public static PoolManager Pool { get { return poolManager; } }

    public PlayerMove player;
    public PlayerMove Player
    {
        get
        {
            if (player == null) 
                player = FindObjectOfType<PlayerMove>();

            return player;
        }
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        InitManager();
    }

    private void OnDestroy()
    {
        if(instance == this)
            instance = null;
    }

    private void InitManager()
    {
        GameObject poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        poolManager = poolObj.AddComponent<PoolManager>();
    }
    
}
