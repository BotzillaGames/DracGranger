using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Time to wait between spawns.
    private float ttw = 10;

    // Time to increase rate.
    private float tti = 30;

    private float internalCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }


    void Update()
    {
        internalCounter += Time.deltaTime;
    }

    private IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            if (internalCounter >= tti)
            {
                internalCounter = 0;
                ttw = ttw / 1.5f;
            }
            SpawnEnemy();
            yield return new WaitForSeconds(ttw);
        }
        yield return 0;
    }


    void SpawnEnemy()
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
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.localPosition = initPos;
        newEnemy.transform.parent = transform;
    }
}
