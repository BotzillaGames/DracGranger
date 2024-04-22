using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    // Time between spawns.
    public float timeBetweenSpawns = 5f;

    // Time to increase spawn rate.
    private float timeToIncreaseRate = 35f;

    // Internal counter for time.
    private float internalCounter = 0;

    // Number of enemies per wave.
    public int enemiesPerWave = 1;

    // Increment in enemies per wave.
    public int enemiesIncrement = 2;

    // Time between waves.
    public float timeBetweenWaves = 30f;

    public GameOverManager gameOver;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {
        internalCounter += Time.deltaTime;
    }

    private IEnumerator SpawnEnemyLoop()
    {
        while (!gameOver.isFinished)
        {
            if (internalCounter >= timeToIncreaseRate)
            {
                internalCounter = 0;
                timeBetweenWaves = timeBetweenWaves / 1.5f;
            }

            // Spawn enemies for the current wave
            for (int i = 0; i < enemiesPerWave; i++)
            {
                GameObject newEnemy = SpawnEnemy();
                spawnedEnemies.Add(newEnemy);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            // Wait between waves
            yield return new WaitForSeconds(timeBetweenWaves);

            // Increase enemies for the next wave
            enemiesPerWave += enemiesIncrement;
        }
    }

    GameObject SpawnEnemy()
    {
        Vector2 initPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        if (Random.value < 0.5f)
        {
            initPos.x = Random.value < 0.5f ? 10 : -10;
        }
        else
        {
            initPos.y = Random.value < 0.5f ? 7 : -7;
        }
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemy]);
        newEnemy.transform.localPosition = initPos;
        newEnemy.transform.parent = transform;
        return newEnemy;
    }

    public void DeleteAllEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear(); // Clear the list of spawned enemies
    }

    public void RemoveEnemyFromList(GameObject enemyToRemove)
    {
        if (spawnedEnemies.Contains(enemyToRemove))
        {
            spawnedEnemies.Remove(enemyToRemove);
        }
    }

    public void StartSpawningEnemies(){
        StartCoroutine(SpawnEnemyLoop());
    }
}
