using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f;

    private bool isPaused = false;

    private void Update()
    {
        if(!isPaused){
            elapsedTime += Time.deltaTime;
        }
    }

    // Method to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void SetPaused(bool paused){
        isPaused = paused;
    }
}
