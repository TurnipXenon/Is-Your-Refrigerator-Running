using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/Stop Agent")]
public class StopAgentTask : Task
{
    [Header("Receiving")]
    public ContextName navMeshAgentName;

    /** Running: On the way
        Success: Reached destination
        Failure: Can't go */
    public override NodeState Evaluate(Context context)
    {
        NavMeshAgent agent = context.Get<NavMeshAgent>(navMeshAgentName);
        NodeState nodeState = NodeState.Failure;

        if (agent != null)
        {
            agent.isStopped = true;
            nodeState = NodeState.Success;
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }
}