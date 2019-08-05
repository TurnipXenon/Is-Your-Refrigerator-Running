using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BehaviourTree/Tasks/Set Value Context")]
public class SetValueContext : Task
{
    [Header("Value")]
    public ContextName valueName;

    [Header("Sending")]
    public ContextName keyName;

    public override NodeState Evaluate(Context context)
    {
        context.Set<ContextName>(keyName, valueName);
        SetNodeState(context, NodeState.Success);
        return NodeState.Success;
    }
}