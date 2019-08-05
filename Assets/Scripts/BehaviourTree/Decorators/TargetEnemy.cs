// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
using UnityEngine;

[CreateAssetMenu(menuName ="BehaviourTree/Decorator/TargetEnemy")]
public class TargetEnemy : Decorator
{
    [Header("Receiving")]
    public ContextName enemyManagerName;

    [Header("Sending")]
    public ContextName locationName;

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */
    public override NodeState Evaluate(Context context)
    {
        CharacterManager enemyManager = context.Get<CharacterManager>(enemyManagerName);
        Transform location = enemyManager?.transform;
        NodeState nodeState = NodeState.Failure;

        if (location != null)
        {
            context.Set<Transform>(locationName, location);
            nodeState = node.Evaluate(context);
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }
}