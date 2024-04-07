using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    private GameObject enemy;

    public Enemy[] allEnemies;

    public GameObject terrain;

    public GameObject max;
    public GameObject min;

    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        InvokeRepeating("SpawnEnemy", 1, 2);
    }

    private void SpawnEnemy()
    {
        enemy = ChooseEnemy();

        GameObject newEnemy = Instantiate(enemy);  
        
        Vector3 position;
        //Escull quina cantonada utilitzarem
        int randomValue = Random.Range(1, 4);
        switch (randomValue) {
        case 1:
            position = new Vector3(min.transform.position.x, Random.Range(min.transform.position.y, max.transform.position.y),1);
            break;
        case 2:
            position = new Vector3(Random.Range(min.transform.position.x, max.transform.position.x), min.transform.position.y,1);
            break;
        case 3:
            position = new Vector3(max.transform.position.x, Random.Range(min.transform.position.y, max.transform.position.y),1);
            break;
        case 4:
            position = new Vector3(Random.Range(min.transform.position.x, max.transform.position.x), max.transform.position.y,1);
            break;
        default:
            position = new Vector3(min.transform.position.x, min.transform.position.y, 1);
            break;
        }
     
        newEnemy.transform.parent = grid.transform;
        Vector3Int cellPosition = grid.LocalToCell(position);
        newEnemy.transform.localPosition = grid.GetCellCenterLocal(cellPosition);

        newEnemy.GetComponent<EnemicController>().SetInitialPosition(cellPosition);
    }


    private GameObject ChooseEnemy(){
        return allEnemies[Random.Range(0, allEnemies.Length)].prefab;
    }
}
