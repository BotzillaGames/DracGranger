using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TutorialIntroManager : MonoBehaviour
{
    public GameController gameController;

    public UIController uIController;

    public UnityEngine.UI.Image step1;
    public UnityEngine.UI.Image step2;
    public UnityEngine.UI.Image instruccions;
    public UnityEngine.UI.Image step3;

    private int step = 1;
    void Start()
    {
        uIController.SetUIVisibility(false);
        ShowTutorialStep(step);
    }


    public void OnClickBackgroundTutorial(CallbackContext context)
    {
        if (context.canceled)
        {
            step++;
            ShowTutorialStep(step);
        }
    }

    public void ShowTutorialStep(int step)
    {
        switch (step)
        {
            case 1:
                step1.color = new Color(1, 1, 1, 1f);
                break;
            case 2:
                LeanTween.value(step1.gameObject, 1, 0, 1).setOnUpdate((val) =>
                {
                    step1.color = new Color(1, 1, 1, val);
                });

                LeanTween.value(step2.gameObject, 0, 1, 1).setOnUpdate((val) =>
                {
                    step2.color = new Color(1, 1, 1, val);
                });

                LeanTween.value(instruccions.gameObject, 0, 1, 1).setOnUpdate((val) =>
                {
                    instruccions.color = new Color(1, 1, 1, val);
                });
                break;
            case 3:
                LeanTween.value(step2.gameObject, 1, 0, 1).setOnUpdate((val) =>
                {
                    step2.color = new Color(1, 1, 1, val);
                });

                LeanTween.value(instruccions.gameObject, 1, 0, 1).setOnUpdate((val) =>
                {
                    instruccions.color = new Color(1, 1, 1, val);
                });

                LeanTween.value(step3.gameObject, 0, 1, 1).setOnUpdate((val) =>
                {
                    step3.color = new Color(1, 1, 1, val);
                });
                break;
            case 4:
                LeanTween.value(step3.gameObject, 1, 0, 1).setOnUpdate((val) =>
                {
                    step3.color = new Color(1, 1, 1, val);
                });
                uIController.SetUIVisibility(true);
                gameController.StartGame();
                break;
            default:
                break;
        }
    }
}
