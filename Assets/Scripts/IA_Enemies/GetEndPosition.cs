using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;


namespace BBUnity.Actions
{

[Action("Custom/GetEnemyEndPosition")]
[Help("Getsthe end position for the enemy")]
public class GetEndPosition : GOAction
{
    [OutParam("endPosition")]
    public Vector3 endPosition { get; set; }

    public Vector3 initialPosition;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public override void OnStart()
    {
        initialPosition = gameObject.transform.position;

        GameObject[] roses = GameObject.FindGameObjectsWithTag("Rosa");
        if(roses.Length > 0){
            endPosition = roses[Random.Range(0, roses.Length)].transform.position;
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
}
