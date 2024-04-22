using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public CustomCursor cursorStatus;
    public GameController gameController;
    public AudioManager audioManager;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            cursorStatus.StartClick(audioManager);
        }
        if (context.canceled)
        {
            cursorStatus.EndClick();
        }
        if (!context.canceled) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) return;

        // If rose send click.
        Rose rose = rayHit.collider.GetComponent<Rose>();
        if (rose)
        {
            int pointsToAdd = rose.ClickRose(cursorStatus.currentCursor);
            gameController.AddPoints(pointsToAdd);
            cursorStatus.OnCursorChange("Default");
        }

        // If UI button send click.
        UIButton uiButton = rayHit.collider.GetComponent<UIButton>();
        if (uiButton)
        {
            uiButton.OnClick();
        }

        ReplayButton gameOver = rayHit.collider.GetComponent<ReplayButton>();
        if (gameOver)
        {
            gameOver.OnClickReplay();
        }

        InstruccionsButton instruccions = rayHit.collider.GetComponent<InstruccionsButton>();
        if (instruccions)
        {
            instruccions.OnClickInstructions();
        }
    }
}
