using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleAnimation : MonoBehaviour
{
    private SpriteRenderer sr; private SpriteRenderer parentSr;
    private float valueToScale = 1.5f;
    private float initialScale = 4.1f;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        parentSr = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.enabled = parentSr.enabled;
    }

    public void WateringAnimation()
    {
        LeanTween.value(gameObject, initialScale, valueToScale, 8).setOnUpdate((val) =>
        {
            transform.localScale = new Vector3(val, val, val);
        });
    }
}
