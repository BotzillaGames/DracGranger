using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TutorialIntroManager : MonoBehaviour
{
    public GameController gameController;

    public GameObject pointAndTime;

    public GameObject abilites;

    public GameObject help;
    public GameObject drac;
    public UnityEngine.UI.Image step1;
    public UnityEngine.UI.Image step2;
    public UnityEngine.UI.Image instruccions;
    public UnityEngine.UI.Image step3;

    public UnityEngine.UI.Image background;

    private int step = 1;
    void Start()
    {
        drac.SetActive(false);
        help.SetActive(false);
        abilites.SetActive(false);
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
                background.color = new Color(0, 0, 0, 0.1f);
                break;
            case 2:
                abilites.SetActive(true);
                LeanTween.value(background.gameObject, 0.1f, 0.5f, 1).setOnUpdate((val) =>
                {
                    background.color = new Color(0, 0, 0, val);
                });

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
                background.color = new Color(0, 0, 0, 0);
                gameController.StartGame();
                drac.SetActive(true);
                help.SetActive(true);
                abilites.SetActive(true);
                break;
            default:
                break;
        }
    }
}
