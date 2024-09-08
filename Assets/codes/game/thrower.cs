using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class thrower : MonoBehaviour
{
    public GameObject candyPrefab; // The prefab to instantiate
    public float spawnInterval = 1.33f; // Time interval between spawns
    public float throwTime = 0.48f;
    public Image[] candySprites;

    public bool isThrowing = true;

    void SpawnCandy()
    {
        if (isThrowing)
        {
            // Instantiate the candy UI element
            GameObject Bullet = Instantiate(candyPrefab, transform);
            Bullet.transform.parent = GameObject.Find("spawnBullet").transform;
        }

    }
    public void StartThrowing()
    {
        // Start invoking the SpawnCandy method repeatedly
        InvokeRepeating("StartAttack", 0, spawnInterval);

    }

    public void StartAttack()
    {
        Invoke("SpawnCandy", throwTime);
    }
}
