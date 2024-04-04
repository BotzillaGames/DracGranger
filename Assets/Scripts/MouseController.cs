using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    private Grid grid;

    private SpriteRenderer hoverSprite;

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        hoverSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Click(true);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Click(false);
        }
    }

    private void Hover()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        worldPosition.z = 0;
        Debug.DrawLine(Camera.main.transform.position, worldPosition);
        Vector3Int cellPosition = grid.LocalToCell(new Vector2(worldPosition.x, worldPosition.y));
        hoverSprite.transform.localPosition = grid.GetCellCenterLocal(cellPosition);
    }

    private void Click(bool isDown)
    {
        hoverSprite.color = new Color(hoverSprite.color.r, hoverSprite.color.g, hoverSprite.color.b, isDown ? 0.7f : 0.2f);
    }
}
