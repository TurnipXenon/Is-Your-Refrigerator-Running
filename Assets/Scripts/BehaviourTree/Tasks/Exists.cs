
using UnityEngine;
/**
*  Does the given node if result matches wanted result
*/
[CreateAssetMenu(menuName = "BehaviourTree/Tasks/Exists")]
public class Exists : Task
{
    [Header("Receiving")]
    public ContextName keyName;

    public override NodeState Evaluate(Context context)
    {
        NodeState result = NodeState.Failure;

        if (context.Exists(keyName))
        {
            result = NodeState.Success;
        }

        SetNodeState(context, result);
        return result;
    }
}