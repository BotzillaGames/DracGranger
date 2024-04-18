using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{

    public enum CursorType { Seed, Water, Cut, Fire, Default }

    public Sprite[] cursors;

    public CursorType currentCursor = CursorType.Default;

    private Image imgCursor;

    // Start is called before the first frame update
    void Start()
    {
        imgCursor = GetComponent<Image>();
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
                imgCursor.sprite = cursors[(int)CursorType.Seed];
                break;
            case "Water":
                currentCursor = CursorType.Water;
                imgCursor.sprite = cursors[(int)CursorType.Water];
                break;
            case "Cut":
                currentCursor = CursorType.Cut;
                imgCursor.sprite = cursors[(int)CursorType.Cut];
                break;
            case "Fire":
                currentCursor = CursorType.Fire;
                imgCursor.sprite = cursors[(int)CursorType.Fire];
                break;
            case "Default":
                currentCursor = CursorType.Default;
                imgCursor.sprite = cursors[(int)CursorType.Default];
                break;
        }
    }
}
