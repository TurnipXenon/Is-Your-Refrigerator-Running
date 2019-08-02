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

    /** Running: On the way
        Success: Reached final destination
        Failure: Can't go */
    public override NodeState Evaluate(Context context)
    {
        Patrol patrol = context.Get<Patrol>(patrolName);

        if (patrol.currentIndex < 0 && patrol.currentIndex >= patrol.waypointList.childCount)
        {
            Debug.LogWarning("Invalid index: Index is at " + patrol.currentIndex.ToString()
                + " when transform child count is " + patrol.waypointList.childCount.ToString());
            SetNodeState(context, NodeState.Failure);
            return NodeState.Failure;
        }

        context.Set<NavMeshAgent>(navMeshAgentName, patrol.agent);
        context.Set<Transform>(locationName, 
            patrol.waypointList.GetChild(patrol.currentIndex));

        NodeState result = node.Evaluate(context);

        if (result == NodeState.Success)
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
                result = node.Evaluate(context);
            }
        }

        SetNodeState(context, result);
        return result;
    }
}