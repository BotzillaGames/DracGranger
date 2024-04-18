using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public CustomCursor.CursorType type;

    public CustomCursor customCursor;

    public void OnClick()
    {
        customCursor.OnCursorChange(type.ToString());
    }
}
