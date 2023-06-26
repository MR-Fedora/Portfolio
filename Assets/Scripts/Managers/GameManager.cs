using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isLive;
    public float gameTime = 0;
    public float maxTime = 10f;
    public int playerId;
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

    public PlayerBox box;
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
        InitManager();
    }
    public void GameStart(int id)
    {
        playerId = id;
        gameTime = 0;
        box = FindObjectOfType<PlayerBox>();
        player = box.playerBox;
        uiLevelUp = FindObjectOfType<LevelUp>();
        poolManager.poolRoot = new GameObject("PoolRoot").transform;
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId%2);
        isLive = true;
        Resume();
    }
    public void GameReTry()
    {
        poolManager.poolDic.Clear();
        poolManager.poolContainer.Clear();
        exp = 0;
        level = 0;
        kill = 0;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        player.overUI.gameObject.SetActive(true);
        player.overUI.Lose();
        Stop();
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        player.enemyClear.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        player.overUI.gameObject.SetActive(true);
        player.overUI.Win();
        Stop();
    }
   
    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime > maxTime)
        {
            gameTime = maxTime;
            GameVictory();
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
        if(!isLive)
            return;
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
