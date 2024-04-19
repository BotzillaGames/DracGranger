using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoseSpawner : MonoBehaviour
{

    private const int LIFESTART = 5;

    public GameObject rose;

    public GameOverManager gameOver;

    private Grid grid;

    private List<Vector2> usedPoints = new List<Vector2>();

    private List<Rose> roses = new List<Rose>();

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        for (int i = 0; i < LIFESTART; i++)
        {
            SpawnRose();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!gameOver.isFinished)
        {
            bool gameOver = true;
            roses.ForEach((rose) =>
            {
                if (!rose.isDead)
                {
                    gameOver = false;
                }
            });
            if (gameOver) FinishGame();
        }

    }

    private void SpawnRose()
    {
        bool emptySpaceFound = false;
        Vector3 randomWorldPoint = new Vector3();
        while (!emptySpaceFound)
        {
            randomWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.25f, 0.85f), Random.Range(0.25f, 0.85f), 0));
            randomWorldPoint.z = 0;
            if (usedPoints.Count == 0) emptySpaceFound = true;
            if (!emptySpaceFound)
            {
                emptySpaceFound = true;
                Vector3Int cellPositionTemp = grid.LocalToCell(randomWorldPoint);
                Vector2 snappedPosTemp = grid.GetCellCenterLocal(cellPositionTemp);
                Vector2 randomWorldPointV2 = new Vector2(snappedPosTemp.x, snappedPosTemp.y);
                foreach (Vector2 usedPoint in usedPoints)
                {
                    if (usedPoint == randomWorldPointV2)
                        emptySpaceFound = false;
                }
            }
        }

        GameObject newRose = Instantiate(rose);
        newRose.transform.parent = grid.transform;
        Vector3Int cellPosition = grid.LocalToCell(randomWorldPoint);
        Vector2 snappedPos = grid.GetCellCenterLocal(cellPosition);
        newRose.transform.localPosition = snappedPos;
        usedPoints.Add(snappedPos);
        roses.Add(newRose.GetComponent<Rose>());
    }

    private void FinishGame()
    {
        if (!gameOver.isFinished)
        {
            gameOver.FinishGame();
            gameOver.isFinished = true;
        }
    }
}
