
using UnityEngine;
using UnityEngine.AI;

public class EnemicController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
