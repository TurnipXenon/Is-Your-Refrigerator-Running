using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/GoTo")]
public class GoToTask : Task
{
    [Header("Receiving")]
    public ContextName navMeshAgentName;
    public ContextName locationName;

    private NavMeshAgent agent;
    private Transform location;

    /** Running: On the way
        Success: Reached destination
        Failure: Can't go */
    public override NodeState Evaluate(Context context)
    {
        agent = context.Get<NavMeshAgent>(navMeshAgentName);
        location = context.Get<Transform>(locationName);

        if (locationName == null || agent == null)
        {
            SetNodeState(context, NodeState.Failure);
            return nodeState;
        }

        agent.destination = location.position;
        agent.isStopped = false;

        if (agent.remainingDistance <= agent.stoppingDistance
            && !agent.pathPending)
        {
            SetNodeState(context, NodeState.Success);
        }
        else
        {
            SetNodeState(context, NodeState.Running);
        }

        return nodeState;
    }
}