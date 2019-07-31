using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/GoTo")]
public class GoToTask : Task
{
    [Header("Receiving")]
    public ContextName navMeshAgentName;
    public ContextName locationName;

    /** Running: On the way
        Success: Reached destination
        Failure: Can't go */
    public override NodeState Evaluate(Context context)
    {
        NavMeshAgent agent = context.Get<NavMeshAgent>(navMeshAgentName);
        Transform location = context.Get<Transform>(locationName);

        if (locationName == null || agent == null)
        {
            return NodeState.Failure;
        }

        agent.destination = location.position;
        agent.isStopped = false;

        if (agent.remainingDistance <= agent.stoppingDistance
            && !agent.pathPending)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}