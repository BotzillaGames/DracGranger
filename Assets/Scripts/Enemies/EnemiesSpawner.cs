using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    private Enemy enemy;

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

        GameObject newEnemy = Instantiate(enemy.prefab);  
        
        Vector3 position;
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
        newEnemy.GetComponent<EnemyController>().SetInitialPosition(cellPosition);
        newEnemy.GetComponent<EnemyController>().InitializateEnemyValues(enemy, DecideDirection(position));
    }

    public int DecideDirection(Vector3 position)
    {
        // Considera los bordes del tablero
        int minAngleX = -45;
        int maxAngleX = 45;
        int minAngleY = -45;
        int maxAngleY = 45;

        // Si está en el borde izquierdo, evitar ir más a la izquierda
        if (position.x <= min.transform.position.x)
        {
            minAngleX = 0; // No puede ir hacia -45 grados en el eje X
        }

        // Si está en el borde derecho, evitar ir más a la derecha
        if (position.x >= max.transform.position.x)
        {
            maxAngleX = 0; // No puede ir hacia 45 grados en el eje X
        }

        // Si está en el borde inferior, evitar ir más hacia abajo
        if (position.y <= min.transform.position.y)
        {
            minAngleY = 0; // No puede ir hacia -45 grados en el eje Y
        }

        // Si está en el borde superior, evitar ir más hacia arriba
        if (position.y >= max.transform.position.y)
        {
            maxAngleY = 0; // No puede ir hacia 45 grados en el eje Y
        }

        // Decisión final de ángulo basado en los límites calculados
        if (minAngleX == 0 && maxAngleX == 0 && minAngleY == 0 && maxAngleY == 0)
        {
            return 0; // Solo puede ir hacia adelante
        }
        else if (minAngleX == 0)
        {
            // Si está en el borde izquierdo, solo puede ir hacia arriba o hacia la derecha
            return DecideRandomAngle(new int[] { 0, 45 });
        }
        else if (maxAngleX == 0)
        {
            // Si está en el borde derecho, solo puede ir hacia abajo o hacia la izquierda
            return DecideRandomAngle(new int[] { 180, -45 });
        }
        else if (minAngleY == 0)
        {
            // Si está en el borde inferior, solo puede ir hacia la derecha o hacia arriba
            return DecideRandomAngle(new int[] { 90, 45 });
        }
        else if (maxAngleY == 0)
        {
            // Si está en el borde superior, solo puede ir hacia la izquierda o hacia abajo
            return DecideRandomAngle(new int[] { -90, -45 });
        }
        else
        {
            // Puede ir en cualquier dirección
            return DecideRandomAngle(new int[] { minAngleX, minAngleY, maxAngleX, maxAngleY });
        }
    }

    private int DecideRandomAngle(int[] possibleAngles)
    {
        int index = Random.Range(0, possibleAngles.Length);
        return possibleAngles[index];
    }


    private Enemy ChooseEnemy(){
        return allEnemies[Random.Range(0, allEnemies.Length)];
    }
}
