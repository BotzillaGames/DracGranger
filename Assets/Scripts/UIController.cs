using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public TMP_Text timerText;
    public TMP_Text pointsText;

    public Timer timer;

    public MouseController mouseController;

    public Slider slider;

    public GameObject uiParent;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        pointsText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        UpdateSliderValue();
    }


    private void UpdateTimerUI()
    {
        // Get the elapsed time from the Timer script
        float elapsedTime = timer.GetElapsedTime();

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Update the UI Text with the formatted time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdatePoints(int numPoints)
    {
        pointsText.text = numPoints.ToString();
    }

    private void UpdateSliderValue()
    {
        float energyValue = mouseController.GetFireEnergyValue();

        slider.value = energyValue/100;
    }

    public void SetUIVisibility(bool visibility){
        uiParent.SetActive(visibility);
    }
}
