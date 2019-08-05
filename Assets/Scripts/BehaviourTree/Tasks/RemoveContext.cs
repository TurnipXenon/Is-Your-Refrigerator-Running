
using UnityEngine;
/**
*  Does the given node if result matches wanted result
*/
[CreateAssetMenu(menuName = "BehaviourTree/Tasks/Remove Context")]
public class RemoveContext : Task
{
    [Header("Receiving")]
    public ContextName keyName;

    public override NodeState Evaluate(Context context)
    {
        NodeState result = NodeState.Success;

        if (context.Exists(keyName))
        {
            context.Remove(keyName);
        }

        SetNodeState(context, result);
        return result;
    }
}