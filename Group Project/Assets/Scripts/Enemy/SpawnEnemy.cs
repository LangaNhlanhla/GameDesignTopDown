using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    [Header("Unity Handles")]
    [SerializeField] GameObject enemyPatterns;
    [SerializeField] Transform spawnPoint;

    [Header("Floats")]
    [SerializeField] float mySpawns;
    [SerializeField] float startTime, enemySpawned, totalEnemies;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] float spawnSince, decrTime, minTime = 0.65f;

    private void Start()
	{

        
	}
    void Update()
    {
        if (mySpawns <= 0 && enemySpawned <= totalEnemies)
        {
            //int random = Random.Range(0, enemyPatterns.Length);
            Instantiate(enemyPatterns, spawnPoint.position, Quaternion.identity);
            enemySpawned++;
            mySpawns = startTime;
            if (startTime > minTime)
                startTime -= decrTime;
        }
        else
        {
            mySpawns -= Time.deltaTime;
        }


    }
}
