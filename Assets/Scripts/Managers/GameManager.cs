using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime = 0;
    public float maxTime = 300f;
    public int level;
    public int exp;
    public int kill;
    public int[] nextExp = { 3, 5, 10, 40, 50, 60, 70, 80, 90, 100 };

    public static PlayerData playerData;
    public static PoolManager poolManager;
    public static ResourceManager resourceManager;
    
    public static GameManager Instance { get { return instance; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static PlayerData PlayerData { get { return playerData; } }

    public PlayerMove player;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        player = FindObjectOfType<PlayerMove>();
        InitManager();
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxTime)
        {
            gameTime = maxTime;
        }
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

        GameObject resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        resourceManager = resourceObj.AddComponent<ResourceManager>();

        GameObject playerObj = new GameObject();
        playerObj.name = "PlayerData";
        playerObj.transform.parent = transform;
        playerData = playerObj.AddComponent<PlayerData>();
    }
    public void GetExp()
    {
       exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp =0;
        }
    }
}
