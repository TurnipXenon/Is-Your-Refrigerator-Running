using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/Randomizer")]
public class Randomizer : Task
{
    [Header("Receiving")]
    public ContextName setName;

    [Header("Sending")]
    public ContextName chosenItemName;

    public override NodeState Evaluate(Context context)
    {
        Object[] set = context.Get<Object[]>(setName);
        if (set != null && set.Length > 0)
        {
            context.Set<Object>(chosenItemName, set[Random.Range(0, set.Length)]);
            SetNodeState(context, NodeState.Success);
            return NodeState.Success;
        }
        else
        {
            SetNodeState(context, NodeState.Failure);
            return NodeState.Failure;
        }
    }
}