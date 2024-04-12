
using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore; 

public class EnemicController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    private Vector3 initialPosition;
    private Vector3 endPosition;
    private BehaviorExecutor behaviourExecutor;

    private bool isDead = false;
    
    //Suposo que d'es d'aqui restarem les vides?

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        behaviourExecutor = GetComponent<BehaviorExecutor>();

        Destroy(gameObject, 10.0f);
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
            //gameObject.GetComponent<Enemy>().numLifes--;
            //myBlackboard["MeQuemo"] = true;
            Debug.Log("MeQuemo");
            behaviourExecutor.SetBehaviorParam("IsAlive", !isDead);
            behaviourExecutor.SetBehaviorParam("MeQuemo", true);
        }
    }
}
