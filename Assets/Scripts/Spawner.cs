using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;
    float gameTime = 0;
    float maxTime = 3 * 10f;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxTime)
        {
            gameTime = maxTime;
        }
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(gameTime / 10f);
        if(timer > spawnData[level].spawnTime)
        {
            
            timer = 0f;
            Spawn();
        }
    }
    
    private void Spawn()
    {
        GameObject enemy= GameManager.poolManager.Get(spawnData[level].monster);
        enemy.transform.position = spawnPoint[UnityEngine.Random.Range(1,spawnPoint.Length)].transform.position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}
