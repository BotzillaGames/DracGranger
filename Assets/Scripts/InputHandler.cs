using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public CustomCursor cursorStatus;
    public GameController gameController;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) return;

        // If rose send click.
        Rose rose = rayHit.collider.GetComponent<Rose>();
        if (rose)
        {
            bool shouldAddPoints = rose.ClickRose(cursorStatus.currentCursor);
            if (shouldAddPoints) gameController.AddPoints(1);
            cursorStatus.OnCursorChange("Default");
        }

        // If UI button send click.
        UIButton uiButton = rayHit.collider.GetComponent<UIButton>();
        if (uiButton)
        {
            uiButton.OnClick();
        }
    }
}
