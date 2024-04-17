using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rose : MonoBehaviour
{

    private bool mouse_over = false;

    private bool isDead = false;

    private int roseLifeStep; // 0 = Seeds, 1 = Growing, 2 = More growing, 3 = Ready to cut, 4 = Dead. 

    // Start is called before the first frame update
    void Start()
    {
        roseLifeStep = 0;
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


    public bool ClickRose(CustomCursor.CursorType clickCursorType)
    {
        if (!isDead)
        {
            if (roseLifeStep == 0 && clickCursorType == CustomCursor.CursorType.Seed)
            {
                RoseNextStep();
            }
            else if ((roseLifeStep == 1 || roseLifeStep == 2) && clickCursorType == CustomCursor.CursorType.Water)
            {
                RoseNextStep();
            }
            else if (roseLifeStep == 3 && clickCursorType == CustomCursor.CursorType.Cut)
            {
                roseLifeStep = 0;
                RoseNextStep(true);
                return true;
            }
        }
        return false;
    }

    public void SetRoseDead()
    {
        isDead = true;
        transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
        roseLifeStep = 4;
        transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = true;
    }

}


