using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoseSpawner : MonoBehaviour
{

    private const int LIFESTART = 5;

    public GameObject rose;
    public Image dracFace;
    public Sprite[] spritesDrac;
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
            int lifes = 0;
            roses.ForEach((rose) =>
            {
                if (!rose.isDead)
                {
                    lifes++;
                }
            });
            if (lifes == 0) FinishGame();
            if (lifes == 5)
            {
                dracFace.sprite = spritesDrac[0];
            }
            else if (lifes > 1)
            {
                dracFace.sprite = spritesDrac[1];
            }
            else if (lifes > 0)
            {
                dracFace.sprite = spritesDrac[2];
            }
        }

    }

    private void SpawnRose()
    {
        bool emptySpaceFound = false;
        Vector3 randomWorldPoint = new Vector3();
        while (!emptySpaceFound)
        {
            randomWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 0));
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

    public void ResetRoseSpawner(){
        CleanAndDestroyRoses();
        
        grid = transform.parent.GetComponent<Grid>();
        for (int i = 0; i < LIFESTART; i++)
        {
            SpawnRose();
        }
    }

    private void CleanAndDestroyRoses()
    {
        usedPoints.Clear();

        foreach (Rose rose in roses)
        {
            if (rose != null && rose.gameObject != null)
            {
                Destroy(rose.gameObject);
            }
        }
        roses.Clear();
    }
}
