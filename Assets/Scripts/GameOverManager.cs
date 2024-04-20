using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public bool isFinished = false;

    public GameObject parent;

    public UnityEngine.UI.Image background;

    public TextMeshProUGUI gameOverText;

    public UnityEngine.UI.Image gameOverButton;

    public TextMeshProUGUI gameOverButtonText;

    public void FinishGame()
    {
        Debug.Log("Game Over!!");
        parent.SetActive(true);
        LeanTween.value(background.gameObject, 0, 0.8f, 1f).setOnUpdate((val) =>
        {
            background.color = new Color(0, 0, 0, val);
        });
        LeanTween.value(gameOverText.gameObject, 0, 1, 1).setDelay(0.7f).setOnUpdate((val) =>
        {
            gameOverText.color = new Color(1, 1, 1, val);
        });
        LeanTween.value(gameOverButton.gameObject, 0, 1, 1).setDelay(1.5f).setOnUpdate((val) =>
        {
            gameOverButton.color = new Color(1, 1, 1, val);
        });
        LeanTween.value(gameOverButtonText.gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
        {
            gameOverButtonText.color = new Color(0.1f, 0.1f, 0.1f, val);
        });
    }
}
