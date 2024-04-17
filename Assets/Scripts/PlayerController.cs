using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Grid grid;

    private Animator sjAnimator;

    private Vector2 playerPos;

    private Vector2 destination;

    private bool burning = false;

    private int life = 3;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        sjAnimator = GetComponentInChildren<Animator>();
        destination = transform.localPosition * -1;
        if (destination.x > transform.localPosition.x)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        Debug.DrawLine(Vector2.zero, destination, Color.red, 10);
        InvokeRepeating("LeanMove", 2.0f, 2f);
    }

    private void LeanMove()
    {
        if (!burning)
        {
            sjAnimator.SetBool("idleAnim", false);
            Vector3Int cellPosition = grid.LocalToCell(new Vector2(transform.localPosition.x + destination.normalized.x * 2, transform.localPosition.y + destination.normalized.y * 2));
            LeanTween.moveLocal(gameObject, grid.GetCellCenterLocal(cellPosition), .5f).setDelay(1).setEaseOutExpo().setOnComplete(() =>
            {
                sjAnimator.SetBool("idleAnim", true);
            });
        }

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire" && !burning)
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                burning = true;
                GoBack();
            }
        }
        else if (other.tag == "Rose" && !burning)
        {
            other.GetComponent<Rose>().SetRoseDead();
        }
    }

    private void GoBack()
    {
        Vector2 backPos = new Vector2(transform.localPosition.x - destination.normalized.x * 2, transform.localPosition.y - destination.normalized.y * 2);
        Vector3Int cellPosition = grid.LocalToCell(backPos);
        LeanTween.moveLocal(gameObject, grid.GetCellCenterLocal(cellPosition), 3).setDelay(1).setEaseOutElastic().setOnComplete(() =>
        {
            burning = false;
        });
    }
}
