using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int points = 0;

    private int bestScore = 0;
    public Timer timer;
    public UIController uiController;
    public RoseSpawner roseSpawner;
    public EnemySpawner enemySpawner;


    void Start()
    {

    }

    public void AddPoints(int numPointsToAdd)
    {
        points += numPointsToAdd;
        uiController.UpdatePoints(points);
    }

    public int GetScore(){
        return points;
    }

    public void PauseTime(bool pause){
        timer.SetPaused(pause);
    }

    public void ResetGame(){
        timer.ResetTimer();
        PauseTime(false);
        points = 0;
        uiController.UpdatePoints(points);
        roseSpawner.ResetRoseSpawner();
        enemySpawner.DeleteAllEnemies();
    }

    public void StartGame(){
        timer.ResetTimer();
        uiController.UpdatePoints(points);
        enemySpawner.StartSpawningEnemies();
    }
}
