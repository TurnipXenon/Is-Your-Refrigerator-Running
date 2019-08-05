using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/IsEnemyNearTask")]
public class IsEnemyNearTask : Task
{
    [Header("Receiving")]
    public ContextName userTransformName;
    public ContextName enemyListName;
    public ContextName dangerSqrDistanceName;

    [Header("Sending")]
    public ContextName enemyManagerName;
    public ContextName enemyFreshnessName;
    public ContextName userFreshnessName;

    /** Success: Enemy is near within the threshold
        Failure: Enemy is faraway */
    public override NodeState Evaluate(Context context)
    {
        Transform userTransform = context.Get<Transform>(userTransformName);
        List<Transform> enemyList = context.Get<List<Transform>>(enemyListName);
        float dangerSqrDistance = context.Get<float>(dangerSqrDistanceName);
        NodeState nodeState = NodeState.Failure;

        if (enemyList != null && userTransform != null)
        {
            foreach (Transform enemyTransform in enemyList)
            {
                // item can be null due to destroy???
                if (enemyTransform != null && SqrDistance(enemyTransform, userTransform) < dangerSqrDistance)
                {
                    nodeState = NodeState.Success;
                    CharacterManager enemyManager = enemyTransform.GetComponentInChildren<CharacterManager>();
                    context.Set<CharacterManager>(enemyManagerName, enemyManager);
                    context.Set<float>(enemyFreshnessName, enemyManager.freshnessEndTime);
                    context.Set<float>(userFreshnessName, userTransform.GetComponentInChildren<CharacterManager>().freshnessEndTime);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Enemy list and / or user transform not set in " + name);
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }

    private float SqrDistance(Transform a, Transform b)
    {
        return (a.position - b.position).sqrMagnitude;
    }
}