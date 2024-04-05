using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareAnimation : MonoBehaviour
{
    private SpriteRenderer sr; private SpriteRenderer parentSr;


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
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 0.3f);
    }
}
