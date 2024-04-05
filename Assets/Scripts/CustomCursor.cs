using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{

    public Sprite[] cursors;

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
                srCursor.sprite = cursors[0];
                break;
            case "Water":
                srCursor.sprite = cursors[1];
                break;
            case "Cut":
                srCursor.sprite = cursors[2];
                break;
            case "Fire":
                srCursor.sprite = cursors[3];
                break;
            case "Default":
                srCursor.sprite = cursors[4];
                break;

        }
    }
}
