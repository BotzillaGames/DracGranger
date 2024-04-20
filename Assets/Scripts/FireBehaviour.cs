using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    private float ttl = 4;
    private float currLive = 0;

    private SpriteRenderer sr;

    private bool isDestroying = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("FlipSprite", 0.1f, 0.1f);
        LeanTween.init(2000);
    }

    private void FlipSprite()
    {
        sr.flipX = !sr.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        currLive += Time.deltaTime;
        if (currLive > ttl && !isDestroying)
        {
            isDestroying = true;
            LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((val) =>
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, val);
            }).setEaseOutExpo().setOnComplete(() =>
            {
                StartCoroutine(DestroyFire());
            });
        }
    }

    private IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
