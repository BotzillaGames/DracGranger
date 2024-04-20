using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rose : MonoBehaviour
{

    private bool mouse_over = false;

    public bool isDead = false;

    private float timeToDry = 8;

    private float timeToGrow = 8;

    private float internalDryCounter = 0;

    private float internalGrowCounter = 0;

    private AudioManager audioManager;

    private int roseLifeStep; // 0 = Seeds, 1 = Growing, 2 = More growing, 3 = Ready to cut one, 4 = Growing Three, 5 = Ready to cut three, 6 = Dry one rose, 7 = Dry three rose, 8 = Dead without rose, 9 = Dead with rose. 

    // Start is called before the first frame update
    void Start()
    {
        roseLifeStep = 0;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if ((roseLifeStep == 2 || roseLifeStep == 4) && !isDead)
        {
            internalGrowCounter += Time.deltaTime;
            if (internalGrowCounter >= timeToGrow)
            {
                RoseNextStep();
                audioManager.GenerateAudio(AudioManager.AudioType.Water);
            }
        }
        if ((roseLifeStep == 3 || roseLifeStep == 5) && !isDead)
        {
            internalDryCounter += Time.deltaTime;
            if (internalDryCounter >= timeToDry)
            {
                transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
                roseLifeStep += 2;
                transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void RoseNextStep(bool reset = false)
    {
        if (reset)
        {
            internalDryCounter = 0;
            internalGrowCounter = 0;
            foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.enabled = false;
            }
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
            SpriteRenderer srToEnable = transform.GetChild(roseLifeStep + 1).GetComponent<SpriteRenderer>();
            srToEnable.enabled = true;
            FlareAnimation flare = srToEnable.GetComponentInChildren<FlareAnimation>();
            if (flare != null)
            {
                internalDryCounter = 0;
                flare.DryingAnimation();
            };
            PuddleAnimation puddle = srToEnable.GetComponentInChildren<PuddleAnimation>();
            if (puddle != null)
            {
                internalGrowCounter = 0;
                puddle.WateringAnimation();
            };
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
            else if ((roseLifeStep == 1 || roseLifeStep == 3) && clickCursorType == CustomCursor.CursorType.Water)
            {
                internalDryCounter = 0;
                audioManager.GenerateAudio(AudioManager.AudioType.Water);
                RoseNextStep();
            }
            else if ((roseLifeStep == 3 || roseLifeStep == 4 || roseLifeStep == 5) && clickCursorType == CustomCursor.CursorType.Cut)
            {
                int pointsToEarn = (roseLifeStep == 3 || roseLifeStep == 4) ? 1 : 3;
                roseLifeStep = 0;
                audioManager.GenerateAudio(AudioManager.AudioType.Cut);
                RoseNextStep(true);
                return pointsToEarn;
            }
            else if ((roseLifeStep == 6 || roseLifeStep == 7) && clickCursorType == CustomCursor.CursorType.Cut)
            {
                roseLifeStep = 0;
                audioManager.GenerateAudio(AudioManager.AudioType.Cut);
                RoseNextStep(true);
                return 0;
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
            if (roseLifeStep == 3 || roseLifeStep == 4 || roseLifeStep == 5 || roseLifeStep == 6 || roseLifeStep == 7)
            {
                roseLifeStep = 9;
            }
            else
            {
                roseLifeStep = 8;
            }
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = true;
            audioManager.BreakRose();
        }
    }

}


