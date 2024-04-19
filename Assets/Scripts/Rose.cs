using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rose : MonoBehaviour
{

    private bool mouse_over = false;

    private bool isDead = false;

    private AudioManager audioManager;

    private int roseLifeStep; // 0 = Seeds, 1 = Growing, 2 = More growing, 3 = Ready to cut one, 4 = Ready to cut three, 5 = Dead without rose, 6 = Dead with rose. 

    // Start is called before the first frame update
    void Start()
    {
        roseLifeStep = 0;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void RoseNextStep(bool reset = false)
    {
        if (reset)
        {
            foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.enabled = false;
            }
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(roseLifeStep + 1).GetComponent<SpriteRenderer>().enabled = true;
            roseLifeStep++;
        }

    }


    public int ClickRose(CustomCursor.CursorType clickCursorType)
    {
        if (!isDead)
        {
            Debug.Log("Click rose | currLifeStep: " + roseLifeStep + " | Current Cursor: " + clickCursorType);
            if (roseLifeStep == 0 && clickCursorType == CustomCursor.CursorType.Seed)
            {
                RoseNextStep();
                audioManager.GenerateAudio(AudioManager.AudioType.Seed);
            }
            else if ((roseLifeStep == 1 || roseLifeStep == 2 || roseLifeStep == 3) && clickCursorType == CustomCursor.CursorType.Water)
            {
                RoseNextStep();
                audioManager.GenerateAudio(AudioManager.AudioType.Water);
            }
            else if ((roseLifeStep == 3 || roseLifeStep == 4) && clickCursorType == CustomCursor.CursorType.Cut)
            {
                roseLifeStep = 0;
                audioManager.GenerateAudio(AudioManager.AudioType.Cut);
                RoseNextStep(true);
                return roseLifeStep == 3 ? 1 : 3;
            }
        }
        return 0;
    }

    public void SetRoseDead()
    {
        if (!isDead)
        {
            isDead = true;
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
            if (roseLifeStep == 3 || roseLifeStep == 4)
            {
                roseLifeStep = 6;
            }
            else
            {
                roseLifeStep = 5;
            }
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = true;
            audioManager.BreakRose();
        }
    }

}


