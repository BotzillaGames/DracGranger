using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rose : MonoBehaviour
{

    private SpriteRenderer roseSprite;

    private float randomGrowTime;

    private float internalRoseLife;
    private bool mouse_over = false;


    private int roseLifeStep; // 0 = Seeds, 1 = Growing, 2 = Ready to cut, 3 = Dead. 

    // Start is called before the first frame update
    void Start()
    {
        roseSprite = GetComponent<SpriteRenderer>();
        GenerateRandomGrowTime();
        roseLifeStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        internalRoseLife += Time.deltaTime;

        if (internalRoseLife > randomGrowTime && roseLifeStep < 3)
        {
            internalRoseLife = 0;
            GenerateRandomGrowTime();
            roseLifeStep++;
            switch (roseLifeStep)
            {
                case 1:
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    roseSprite.enabled = true;
                    roseSprite.color = new Color(0.1f, 1, 1, 0.5f);
                    break;
                case 2:
                    roseSprite.color = new Color(1, 1, 1, 1);
                    break;
                case 3:
                    roseSprite.color = Color.black;
                    break;
                default:
                    break;
            }
        }
    }

    void GenerateRandomGrowTime()
    {
        randomGrowTime = Random.Range(3, 8);
    }

    public void ClickRose()
    {
        if (roseLifeStep == 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy"){
            Debug.Log("OnTriggerEnter2D");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            roseSprite.enabled = true;
            roseSprite.color = Color.black;
            roseLifeStep = 3;
        }
    }
}


