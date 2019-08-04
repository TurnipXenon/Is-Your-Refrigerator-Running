using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Decorator/Patrol")]
public class PatrolDecorator : Decorator
{
    [Header("Receiving")]
    public ContextName patrolName;

    [Header("Sending")]
    public ContextName navMeshAgentName;
    public ContextName locationName;

    private Patrol patrol;

    /** Running: On the way
        Success: Reached final destination
        Failure: Can't go */
    public override NodeState Evaluate(Context context)
    {
        patrol = context.Get<Patrol>(patrolName);

        if (patrol.currentIndex < 0 && patrol.currentIndex >= patrol.waypointList.childCount)
        {
            Debug.LogWarning("Invalid index: Index is at " + patrol.currentIndex.ToString()
                + " when transform child count is " + patrol.waypointList.childCount.ToString());
            SetNodeState(context, NodeState.Failure);
            return nodeState;
        }

        context.Set<NavMeshAgent>(navMeshAgentName, patrol.agent);
        context.Set<Transform>(locationName, 
            patrol.waypointList.GetChild(patrol.currentIndex));

        nodeState = node.Evaluate(context);

        if (nodeState == NodeState.Success)
        {
            patrol.currentIndex++;
            if (patrol.currentIndex >= patrol.waypointList.childCount)
            {
                patrol.currentIndex = 0;
            }
            else
            {
                context.Set<Transform>(locationName,
                    patrol.waypointList.GetChild(patrol.currentIndex));
                nodeState = node.Evaluate(context);
            }
        }

        // for the debugging feature
        SetNodeState(context, nodeState);
        return nodeState;
    }
}