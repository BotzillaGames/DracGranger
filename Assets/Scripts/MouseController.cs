using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public CustomCursor cursorStatus;

    public LineRenderer fireLine;

    public GameObject firePrefab;

    private Vector2 currentMousePos;

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
        currentMousePos = worldPosition;
        Debug.DrawLine(Camera.main.transform.position, worldPosition);
        Vector3Int cellPosition = grid.LocalToCell(new Vector2(worldPosition.x, worldPosition.y));
        hoverSprite.transform.localPosition = grid.GetCellCenterLocal(cellPosition);
    }

    private void Click(bool isDown)
    {
        hoverSprite.color = new Color(hoverSprite.color.r, hoverSprite.color.g, hoverSprite.color.b, isDown ? 0.7f : 0.2f);
        if (cursorStatus.currentCursor == CustomCursor.CursorType.Fire && isDown)
        {
            if (fireLine.enabled)
            {
                DoFire();
            }
            else
            {
                StartCoroutine(DrawFireLine());

            }
        }
    }

    IEnumerator DrawFireLine()
    {
        fireLine.positionCount = 2;
        fireLine.SetPosition(0, currentMousePos);
        fireLine.enabled = true;
        while (cursorStatus.currentCursor == CustomCursor.CursorType.Fire)
        {
            fireLine.SetPosition(1, currentMousePos);
            yield return 0;
        }
        fireLine.enabled = false;
        yield return 0;
    }

    void DoFire()
    {
        Vector2 point1 = new Vector2(fireLine.GetPosition(0).x, fireLine.GetPosition(0).y);
        Vector2 point2 = new Vector2(fireLine.GetPosition(1).x, fireLine.GetPosition(1).y);

        float lineMag = Vector2.Distance(point1, point2);

        List<Vector2> instantiatedFire = new List<Vector2>();

        for (int i = 1; i < Math.Floor(lineMag); i++)
        {
            Debug.DrawLine(point2, point1, Color.yellow, 1000);
            float normal = Mathf.InverseLerp(1, (float)Math.Floor(lineMag), i);
            Vector2 newFirePos = Vector2.Lerp(point1, point2, normal);
            Debug.DrawLine(Vector2.zero, newFirePos, Color.red, 1000);
            GameObject newFire = Instantiate(firePrefab);
            newFire.transform.localPosition = newFirePos;
            instantiatedFire.Add(newFire.transform.localPosition);
        }
        cursorStatus.OnCursorChange("Default");
    }
}
