using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isLive;
    public float gameTime = 0;
    public float maxTime = 300f;
    public int level;
    public int exp;
    public int kill;
    public int[] nextExp = { 3, 5, 10, 10, 10, 10, 10, 10, 10, 10 };

    public static PlayerData playerData;
    public static PoolManager poolManager;
    public static ResourceManager resourceManager;

    public static GameManager Instance { get { return instance; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static ResourceManager Resource { get { return resourceManager; } }
    public static PlayerData PlayerData { get { return playerData; } }

    public PlayerMove player;
    public LevelUp uiLevelUp;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        player = FindObjectOfType<PlayerMove>();
        uiLevelUp = FindObjectOfType<LevelUp>();
        InitManager();
    }
    public void GameReTry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver(GameObject retry)
    {
        StartCoroutine(GameOverRoutine(retry));
    }

    IEnumerator GameOverRoutine(GameObject reTry)
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        reTry.SetActive(true);
        Stop();
    }
    public void GameStart()
    {
        uiLevelUp.Select(0);
        isLive = true;
    }
    private void Update()
    {
        if (!isLive)
            return;
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

        if(exp == nextExp[Mathf.Min(level,nextExp.Length-1)])
        {
            level++;
            exp =0;
            uiLevelUp.Show();
        }
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1f;
    }
}
