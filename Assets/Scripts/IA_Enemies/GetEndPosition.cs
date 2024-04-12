using UnityEngine;

using Pada1.BBCore;    
using Pada1.BBCore.Tasks; 
using Pada1.BBCore.Framework;

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

    int[] anglesToRotat = {-45, 0, 45};

    public override void OnStart()
    {
        initialPosition = gameObject.transform.position;

        Vector3 forwardVector = -gameObject.transform.right;

        Quaternion rotation = Quaternion.Euler(0, 0, anglesToRotat[Random.Range(0, 3)]); 
        Vector3 endRotation = rotation * forwardVector;
        gameObject.transform.rotation = rotation;

        endPosition = initialPosition + (-gameObject.transform.right * 20);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }

}
}