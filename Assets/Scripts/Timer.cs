using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f; // Variable to store the elapsed time

    private void Update()
    {
        // Update the elapsed time every frame
        elapsedTime += Time.deltaTime;
    }

    // Method to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f; // Reset the elapsed time to 0
    }

    // Method to get the current elapsed time
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
