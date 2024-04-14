using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{

    public TMP_Text timerText;
    public TMP_Text pointsText;

    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
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

    public void UpdatePoints(int numPoints){
       pointsText.text = numPoints.ToString();
    }
}
