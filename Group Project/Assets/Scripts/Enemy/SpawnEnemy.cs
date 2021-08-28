using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("External Assets")]
    [SerializeField] ObjectPooler pool;

    [Header("Unity Handles")]
    public GameObject[] enemyPatterns;

    [Header("Floats")]
    [SerializeField]float mySpawns;
    public float startTime;
    public float decrTime;
    public float minTime = 0.65f; //limit
    public float spawnTime = 5f;
    public float spawnSince;
    public float addValue = 15;

    private void Start()
	{
        pool = FindObjectOfType<ObjectPooler>();
	}
	void Update()
    {
        /*if(mySpawns <= 0)
        {
            int random = Random.Range(0, enemyPatterns.Length);
            Instantiate(enemyPatterns[random], transform.position, Quaternion.identity);
            mySpawns = startTime;
            if(startTime > minTime)
            startTime -= decrTime;
        }
        else 
        {
            mySpawns -= Time.deltaTime;
        }*/

        spawnSince += Time.deltaTime;
        if(spawnSince >= spawnTime)
		{
            GameObject prefab = pool.GetPrefab();
            prefab.transform.position = this.transform.position;
            mySpawns++;
            spawnSince = 0f;
        }
        if (mySpawns > addValue)
        {
            spawnTime -= decrTime;
            addValue += 5;
            Debug.Log("Spawns: " + mySpawns + "Value: " + addValue);
        }
        if (spawnTime <= 0.5f)
            spawnTime = 0.5f;
    }

}
