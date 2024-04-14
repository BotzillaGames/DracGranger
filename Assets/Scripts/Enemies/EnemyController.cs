using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    private Vector3 initialPosition;
    private Vector3 endPosition;
    private Vector3 burningPosition;
    int[] anglesToRotat = {-45, 0, 45};

    private bool isDead = false;
    private float velocity;
    private int numLifes;
    private bool isBurning = false;

    private const float fireForce = 3.0f;
    private bool isTriggered = false;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        SetEndPosition();
    }


    private void Update(){
        if(isBurning){
            if(CheckIfItsInThePosition(burningPosition)){
                MoveToEndPosition();
                isBurning = false;
                
            }
        } else {
           if(CheckIfItsInThePosition(endPosition)){
                Destroy(gameObject, 2f);
           }
        }
    }

    public void InitializateEnemyValues(Enemy enemy, int[] angles){
        velocity = enemy.velocity;
        numLifes = enemy.numLifes;
        anglesToRotat = angles;
    }

    public void SetInitialPosition(Vector3 initPosition){
        initialPosition = initPosition;
    }

    public void SetEndPosition(){
        initialPosition = gameObject.transform.position;

        Vector3 forwardVector = -gameObject.transform.right;

        Quaternion rotation = Quaternion.Euler(0, 0, anglesToRotat[Random.Range(0, anglesToRotat.Length)]); 
        Vector3 endRotation = rotation * forwardVector;
        gameObject.transform.rotation = rotation;

        endPosition = initialPosition + (-gameObject.transform.right * 20);

        //Em falta comprovar q van cap al centre

        /*GameObject[] roses = GameObject.FindGameObjectsWithTag("Rose");
        if(roses.Length > 0){
            endPosition = roses[Random.Range(0, roses.Length)].transform.position;
        }*/

        MoveToEndPosition();
    }

    public void MoveToEndPosition(){
        if(agent){
            agent.isStopped = false;
            agent.SetDestination(endPosition);
            agent.velocity = Vector3.Normalize(agent.velocity) * velocity;
        }
    }

    public bool CheckIfItsInThePosition(Vector3 position){
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, position);
        if(distanceToTarget <= 0.5f){
            return true;
        } else {
            return false;
        }
    }


    public void ApplyBurningForce(){
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(initialPosition-transform.position) * fireForce, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Fire" && !isTriggered){
            numLifes--;
            isTriggered = true;
            if(numLifes == 0){
                isDead = true;
                Destroy(gameObject, 1.0f);
            } else {
                isBurning = true;
                agent.isStopped = true;
                ApplyBurningForce();
                Invoke("MoveToEndPosition", 3.0f);
            }
        } else if (other.tag == "Rose"){
            other.gameObject.GetComponent<Rose>().SetRoseDead();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Fire" && isTriggered){
            isTriggered = false;
        }
    }

}