using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    private Vector3 initialPosition;
    private Vector3 endPosition;
    private Vector3 burningPosition;
    int angleToRotate;

    private bool isDead = false;
    private float velocity;
    private int numLifes;
    private bool isBurning = false;

    private const float fireForce = 3.0f;
    private bool isTriggered = false;

    private Rigidbody2D rb;
    private Animator animator;

    private EnemySpawner enemySpawner;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        enemySpawner = FindObjectOfType<EnemySpawner>();

        SetEndPosition();
    }


    private void Update(){
        if(isBurning){
            if(CheckIfItsInThePosition(burningPosition)){
                MoveToEndPosition();
                isBurning = false;
                
            }
        } 


        if(CheckIfItsInThePosition(endPosition)){
            Destroy(gameObject, 2f);
        }
        
    }

    public void InitializateEnemyValues(Enemy enemy, int angle){
        velocity = enemy.velocity;
        numLifes = enemy.numLifes;
        angleToRotate = angle;
    }

    public void SetInitialPosition(Vector3 initPosition){
        initialPosition = initPosition;
    }

    public void SetEndPosition(){
        initialPosition = gameObject.transform.position;

        Vector3 forwardVector = -gameObject.transform.right;

        Quaternion rotation = Quaternion.Euler(0, 0, angleToRotate); 
        Vector3 endRotation = rotation * forwardVector;
        
        endPosition = initialPosition + (-endRotation * 15);

        MoveToEndPosition();
    }

    public void MoveToEndPosition(){
        if(agent){
            agent.isStopped = false;
            agent.SetDestination(endPosition);
            agent.velocity = Vector3.Normalize(agent.velocity) * velocity;
            animator.SetFloat("speed", Mathf.Abs(velocity));
        }
    }

    public bool CheckIfItsInThePosition(Vector3 position){
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, position);
        if(distanceToTarget <= 2.0f){
            return true;
        } else {
            return false;
        }
    }


    public void ApplyBurningForce(){
        rb.AddForce(Vector3.Normalize(initialPosition-transform.position) * fireForce, ForceMode2D.Impulse);
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

    private void OnDestroy()
    {
        if (enemySpawner != null)
        {
            enemySpawner.RemoveEnemyFromList(gameObject);
        }
    }

}