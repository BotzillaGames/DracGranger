using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{

    public enum CursorType { Seed, Water, Cut, Fire, Default }

    public Sprite[] cursors;

    public CursorType currentCursor = CursorType.Default;

    private SpriteRenderer srCursor;

    // Start is called before the first frame update
    void Start()
    {
        srCursor = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        worldPosition.z = 0;
        transform.parent.position = worldPosition;
    }

    public void OnCursorChange(string name)
    {
        switch (name)
        {
            case "Seed":
                currentCursor = CursorType.Seed;
                srCursor.sprite = cursors[(int)CursorType.Seed];
                break;
            case "Water":
                currentCursor = CursorType.Water;
                srCursor.sprite = cursors[(int)CursorType.Water];
                break;
            case "Cut":
                currentCursor = CursorType.Cut;
                srCursor.sprite = cursors[(int)CursorType.Cut];
                break;
            case "Fire":
                currentCursor = CursorType.Fire;
                srCursor.sprite = cursors[(int)CursorType.Fire];
                break;
            case "Default":
                currentCursor = CursorType.Default;
                srCursor.sprite = cursors[(int)CursorType.Default];
                break;
        }
    }
}
