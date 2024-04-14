using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int points = 0;
    public Timer timer;
    public UIController uiController;

    void Start()
    {
        timer.ResetTimer();
        uiController.UpdatePoints(points);
    }

    void Update()
    {
        
    }

    public void AddPoints(int numPointsToAdd){
        points += numPointsToAdd;
        uiController.UpdatePoints(points);
    }
}
