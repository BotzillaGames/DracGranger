using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Grid grid;

    private Vector2 playerPos;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        InvokeRepeating("LeanMoveLeft", 2.0f, 2f);
    }

    private void LeanMoveLeft(){
        Vector3Int cellPosition = grid.LocalToCell(new Vector2(transform.localPosition.x - 2, transform.localPosition.y));
        LeanTween.moveLocal(gameObject, grid.GetCellCenterLocal(cellPosition), .5f).setEaseOutExpo();
    }
}
