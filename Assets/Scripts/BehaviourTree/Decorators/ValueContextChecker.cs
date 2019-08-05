// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
using UnityEngine;

[CreateAssetMenu(menuName ="BehaviourTree/Decorator/Value Context Checker")]
public class ValueContextChecker : Decorator
{
    [Header("Value")]
    public ContextName desiredValueName;
    public ContextName defaultValueName;

    [Header("Receiving")]
    public ContextName keyName;

    /* Evaluates the node under if time is less than given,
     * Otherwise, return Failure*/
    public override NodeState Evaluate(Context context)
    {
        NodeState nodeState = NodeState.Failure;
        bool result = desiredValueName == context.Get<ContextName>(keyName, defaultValueName);
        
        if (result)
        {
            nodeState = node.Evaluate(context);
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }
}