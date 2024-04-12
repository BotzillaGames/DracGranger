using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    private Vector3 initialPosition;
    private Vector3 endPosition;

    private bool isDead = false;
    
    //Suposo que d'es d'aqui restarem les vides?

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    private void Update()
    {

    }

    public void SetInitialPosition(Vector3 initPosition){
        initialPosition = initPosition;
    }

    public void SetEndPosition(Vector3 finalPosition){
        endPosition = finalPosition;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Rosa"){
            isDead = true;
            Destroy(gameObject, 1.0f);
        }
    }

}