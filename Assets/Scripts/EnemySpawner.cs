using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 5f);
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
