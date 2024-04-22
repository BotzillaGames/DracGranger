using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{

    public GameOverManager gameOverManager;

    public void OnClickReplay()
    {
        gameOverManager.PlayAgain();
    }
}
