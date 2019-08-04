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
            SetNodeState(context, NodeState.Failure);
            return NodeState.Failure;
        }

        agent.destination = location.position;
        agent.isStopped = false;
        NodeState nodeState = NodeState.Running;

        if (agent.remainingDistance <= agent.stoppingDistance
            && !agent.pathPending)
        {
            nodeState = NodeState.Success;
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }
}