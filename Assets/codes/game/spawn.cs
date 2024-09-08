using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public bool canSpawn = true;
    public GameObject[] EnemyPrefab;
    public int id;
    public void SpawnEnemy()
    {
        canSpawn = false;
        int theRandom = Random.Range(0, EnemyPrefab.Length);
        GameObject spawnedEnemy = Instantiate(EnemyPrefab[theRandom], transform.position, Quaternion.identity);
        spawnedEnemy.GetComponent<enemyReference>().theCharacter.mySpawn = GetComponent<spawn>();
        spawnedEnemy.transform.parent = transform;
    }
}
