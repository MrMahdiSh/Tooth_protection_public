using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public bool isSpawn;
    public float timer;
    public bool canNotSpawn;
    public float spawnInterval = 3;
    public int spawnCounts = 0;
    public spawn[] spawn;
    public int enemiesCount;
    public int maximusEnemiesCount = 8;
    public bool readyForBoss;
    private bool isBossCame;
    public spawn bossSpawn;
    private float lastSpawnTime;

    // waves
    public int wavesCount = 6;
    public float reduceEveryWave = .5f;
    public float wavesPeriod = 10;

    // boss
    public bool isBoss;

    void Start()
    {
        if (isSpawn)
        {
            isSpawn = false;
            Invoke("startSpawnWithDelay", 1f);
        }

        // lastSpawnTime -= spawnInterval;

        InvokeRepeating("wavesPeriodFunc", wavesCount, wavesPeriod);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            bringBoss();
        }
        
        if (readyForBoss)
        {
            isSpawn = false;
        }

        if (!canNotSpawn && isSpawn && !isBoss)
        {
            timer += Time.deltaTime;
        }

        if (isSpawn && timer >= lastSpawnTime + spawnInterval)
        {
            int spawnInt = randomSpawn();
            if (spawnInt != -1 && enemiesCount < maximusEnemiesCount)
            {
                spawnCounts++;
                enemiesCount++;
                lastSpawnTime = timer;
                canNotSpawn = false;
                spawn[spawnInt].SpawnEnemy();
            }
            else
            {
                canNotSpawn = true;
            }
        }

        if (!isBossCame && readyForBoss && enemiesCount == 0)
        {
            isBossCame = true;
            bringBoss();
        }
    }

    void startSpawnWithDelay()
    {
        isSpawn = true;
    }

    int randomSpawn()
    {
        bool isThereAnyEmptySlot = false;

        foreach (var tehSpawn in spawn)
        {
            if (tehSpawn.canSpawn == true)
            {
                isThereAnyEmptySlot = true;
            }
        }

        if (!isThereAnyEmptySlot)
        {
            return -1;
        }
        else
        {
            int theRandom = Random.Range(0, spawn.Length);
            if (spawn[theRandom].canSpawn)
            {
                return theRandom;
            }
            else
            {
                return randomSpawn();
            }

        }
    }

    public void enemyDeth()
    {
        enemiesCount--;
    }

    public void bringBoss()
    {
        bossSpawn.SpawnEnemy();
    }

    void wavesPeriodFunc()
    {
        if (spawnInterval > 1)
        {
            spawnInterval -= reduceEveryWave;
        }
    }
}
