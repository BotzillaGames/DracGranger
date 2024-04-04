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
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Move(new Vector2(transform.localPosition.x, transform.localPosition.y - 2));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Move(new Vector2(transform.localPosition.x - 2, transform.localPosition.y));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Move(new Vector2(transform.localPosition.x + 2, transform.localPosition.y));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Move(new Vector2(transform.localPosition.x, transform.localPosition.y + 2));
        }
    }

    private void Move(Vector2 newPos){
        Vector3Int cellPosition = grid.LocalToCell(newPos);
        transform.localPosition = grid.GetCellCenterLocal(cellPosition);
        Debug.Log("Move");
    }
}
