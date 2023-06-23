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
    
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);
        if(level >=2)
        {
            level = 2;
        }
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
