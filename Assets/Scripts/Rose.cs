using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rose : MonoBehaviour
{

    private float randomGrowTime;

    private float internalRoseLife;
    private bool mouse_over = false;


    private int roseLifeStep; // 0 = Seeds, 1 = Growing, 2 = Ready to cut, 3 = Dead. 

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomGrowTime();
        roseLifeStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        internalRoseLife += Time.deltaTime;

        if (internalRoseLife > randomGrowTime && roseLifeStep < 4)
        {
            internalRoseLife = 0;
            GenerateRandomGrowTime();
            transform.GetChild(roseLifeStep).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(roseLifeStep + 1).GetComponent<SpriteRenderer>().enabled = true;
            roseLifeStep++;
        }
    }

    void GenerateRandomGrowTime()
    {
        randomGrowTime = Random.Range(3, 8);
    }

    public void ClickRose()
    {
        if (roseLifeStep == 3)
        {
            Destroy(gameObject);
        }
    }

}


