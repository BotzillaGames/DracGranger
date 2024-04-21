using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public bool isFinished = false;

    private int scoreRecord = 0;

    private int scoreActual;

    public GameObject parent;

    //Background Panel
    public UnityEngine.UI.Image background;

    //Score
    public TextMeshProUGUI amountRoses;
    public UnityEngine.UI.Image rosaIcon;

    //Game Over Button
    public UnityEngine.UI.Image gameOverButton;

    //New record Images
    public UnityEngine.UI.Image[] newRecord;
    public TextMeshProUGUI newRecordText;

    // Game Over Screen Images
    public UnityEngine.UI.Image[] millorPuntuacio;
    public TextMeshProUGUI bestScoreText;

    //UI icons to hide
    public GameObject abilities;
    public GameObject dracUI;

    //Game Controller
    public GameController gameController;


    public void Start(){
        //read scoreRecord from json

    }

    public void FinishGame()
    {
        Debug.Log("Game Over!!");
        scoreActual = gameController.GetScore();

        parent.SetActive(true);

        //Hide UI items
        abilities.SetActive(false);
        abilities.SetActive(dracUI);

        //Background
        LeanTween.value(background.gameObject, 0, 0.8f, 1f).setOnUpdate((val) =>
        {
            background.color = new Color(1, 1, 1, val);
        });

        //Game over button
        LeanTween.value(gameOverButton.gameObject, 0, 1, 1).setDelay(1.5f).setOnUpdate((val) =>
        {
            gameOverButton.color = new Color(1, 1, 1, val);
        });
        
        //Amount Roses
        amountRoses.text = amountRoses;
        LeanTween.value(amountRoses.gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
        {
            amountRoses.color = new Color(1, 1, 1, val);
        });

        //Rosa Icon
        LeanTween.value(rosaIcon.gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
        {
            rosaIcon.color = new Color(1, 1, 1, val);
        });

        if(scoreActual > scoreRecord){
            for(int i = 0; i < newRecord.Length; i++){
                LeanTween.value(newRecord[i].gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
                {
                    newRecord[i].color = new Color(1, 1, 1, val);
                });
            }

            LeanTween.value(newRecordText.gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
            {
                newRecordText.color = new Color(0, 0, 0, val);
            });
        } else {
            for(int i = 0; i < millorPuntuacio.Length; i++){
                LeanTween.value(millorPuntuacio[i].gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
                {
                    millorPuntuacio[i].color = new Color(1, 1, 1, val);
                });
            }


            LeanTween.value(bestScoreText.gameObject, 0, 1, 1).setDelay(2f).setOnUpdate((val) =>
            {
                bestScoreText.color = new Color(0, 0, 0, val);
            });
        }
    }
}
