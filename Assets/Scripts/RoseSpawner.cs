using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseSpawner : MonoBehaviour
{

    public GameObject rose;

    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        InvokeRepeating("SpawnRose", 1, 2);
    }

    private void SpawnRose()
    {
        Vector3 randomWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 0));
        randomWorldPoint.z = 0;
        GameObject newRose = Instantiate(rose);
        newRose.transform.parent = grid.transform;
        Vector3Int cellPosition = grid.LocalToCell(randomWorldPoint);
        newRose.transform.localPosition = grid.GetCellCenterLocal(cellPosition);
    }
}
