using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    private float ttl = 5;
    private float currLive = 0;

    private SpriteRenderer sr;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("FlipSprite", 0.1f, 0.1f);
    }

    private void FlipSprite()
    {
        sr.flipX = !sr.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        currLive += Time.deltaTime;
        if (currLive > ttl)
        {
            Destroy(gameObject);
        }
    }
}
