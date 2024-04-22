using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class TutorialManager : MonoBehaviour
{
    public GameOverManager gameOverManager;

    public AudioMixer masterMixer;

    public void OnPausePressed(CallbackContext context)
    {
        if (context.canceled)
        {
            Debug.Log("Pause pressed");
            if (!gameOverManager.isFinished)
            {
                bool isPaused = Time.timeScale == 0;
                if (isPaused)
                {
                    masterMixer.SetFloat("CutoffLowpass", 22000);
                    masterMixer.SetFloat("MasterVolume", 0);
                }
                else
                {
                    masterMixer.SetFloat("CutoffLowpass", 700);
                    masterMixer.SetFloat("MasterVolume", -10);
                }
                transform.GetChild(0).gameObject.SetActive(!isPaused);
                transform.GetChild(1).gameObject.SetActive(!isPaused);
                Time.timeScale = isPaused ? 1 : 0;
            }
        }

    }
}
