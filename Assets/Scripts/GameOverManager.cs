using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public bool isFinished = false;

    private int highScore = 0;
     private string highScoreKey = "HighScore";

    private int scoreActual;

    public GameObject parent;

    //Background Panel
    public UnityEngine.UI.Image background;

    //Score
    public TextMeshProUGUI amountRoses;
    public UnityEngine.UI.Image rosaIcon;

    //Game Over Button
    public UnityEngine.UI.Image gameOverButton;

    //Instructions Button
    public UnityEngine.UI.Image instructionsButton;

    //New record Images
    public UnityEngine.UI.Image[] newRecord;
    public TextMeshProUGUI newRecordText;

    // Game Over Screen Images
    public UnityEngine.UI.Image[] millorPuntuacio;
    public TextMeshProUGUI bestScoreText;

    //UI icons to hide
    public GameObject abilities;
    public GameObject dracUI;

    public UnityEngine.UI.Image blackScreen;

    //Game Controller
    public GameController gameController;


    public void Start(){
        LoadHighScore();

    }

    public void FinishGame()
    {
        Debug.Log("Game Over!!");
        gameController.PauseTime(true);
        scoreActual = gameController.GetScore();

        parent.SetActive(true);

        //Hide UI items
        abilities.SetActive(false);
        dracUI.SetActive(false);

        //Background
        LeanTween.value(background.gameObject, 0, 1f, 1f).setOnUpdate((val) =>
        {
            background.color = new Color(1, 1, 1, val);
        });

        //BlackScreen 
        LeanTween.value(blackScreen.gameObject, 0, 0.5f, 1f).setOnUpdate((val) =>
        {
            blackScreen.color = new Color(0, 0, 0, val);
        });

        //Game over button
        LeanTween.value(gameOverButton.gameObject, 0, 1, 1).setOnUpdate((val) =>
        {
            gameOverButton.color = new Color(1, 1, 1, val);
        });

        //Instructions button
        LeanTween.value(instructionsButton.gameObject, 0, 1, 1).setOnUpdate((val) =>
        {
            instructionsButton.color = new Color(1, 1, 1, val);
        });
        
        //Amount Roses
        amountRoses.text = scoreActual.ToString();
        LeanTween.value(amountRoses.gameObject, 0, 1, 1).setOnUpdate((val) =>
        {
            amountRoses.color = new Color(1, 1, 1, val);
        });

        //Rosa Icon
        LeanTween.value(rosaIcon.gameObject, 0, 1, 1).setOnUpdate((val) =>
        {
            rosaIcon.color = new Color(1, 1, 1, val);
        });

        if(scoreActual > highScore){
            for(int i = 0; i < newRecord.Length; i++){
                int index = i;
                LeanTween.value(newRecord[index].gameObject, 0, 1, 1).setOnUpdate((val) =>
                {
                    newRecord[index].color = new Color(1, 1, 1, val);
                });
            }

            LeanTween.value(newRecordText.gameObject, 0, 1, 1).setOnUpdate((val) =>
            {
                newRecordText.color = new Color(0, 0, 0, val);
            });
            
            highScore = scoreActual;
            SaveHighScore();
        } else {
            for(int i = 0; i < millorPuntuacio.Length; i++){
                int index = i;
                LeanTween.value(millorPuntuacio[index].gameObject, 0, 1, 1).setOnUpdate((val) =>
                {
                    millorPuntuacio[index].color = new Color(1, 1, 1, val);
                });
            }


            LeanTween.value(bestScoreText.gameObject, 0, 1, 1).setOnUpdate((val) =>
            {
                bestScoreText.color = new Color(0, 0, 0, val);
            });
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            highScore = PlayerPrefs.GetInt(highScoreKey);
        }
    }

    public void PlayAgain(){
        Debug.Log("Play Again");

        parent.SetActive(false);

        //Hide UI items
        abilities.SetActive(true);
        dracUI.SetActive(true);

        //Background
        background.color = new Color(1, 1, 1, 0f);

        //BlackScreen 
        blackScreen.color = new Color(0, 0, 0, 0f);

        //Game over button
        gameOverButton.color = new Color(1, 1, 1, 0f);


        //Instructions button
        instructionsButton.color = new Color(1, 1, 1, 0f);
        
        
        //Amount Roses
        amountRoses.color = new Color(1, 1, 1, 0f);

        //Rosa Icon
        rosaIcon.color = new Color(1, 1, 1, 0f);

        for(int i = 0; i < newRecord.Length; i++){
            newRecord[i].color = new Color(1, 1, 1, 0f);
        }
        newRecordText.color = new Color(0, 0, 0, 0f);

        for(int i = 0; i < millorPuntuacio.Length; i++){
            millorPuntuacio[i].color = new Color(1, 1, 1, 0f);
        }

        bestScoreText.color = new Color(0, 0, 0, 0f);

        gameController.ResetGame();
        isFinished = false;
    }

}
