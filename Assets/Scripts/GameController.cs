using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int points = 0;

    private int bestScore = 0;
    public Timer timer;
    public UIController uiController;

    void Start()
    {
        timer.ResetTimer();
        uiController.UpdatePoints(points);
    }

    public void AddPoints(int numPointsToAdd)
    {
        points += numPointsToAdd;
        uiController.UpdatePoints(points);
    }

    public int GetScore(){
        return points;
    }
}
