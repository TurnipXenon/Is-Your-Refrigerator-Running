using UnityEngine;

[CreateAssetMenu(menuName ="BehaviourTree/Decorator/Timer")]
public class Timer : Decorator
{
    [Header("Receiving")]
    public ContextName timerEndName;

    /* Evaluates the node under if time is less than given,
     * Otherwise, return Failure*/
    public override NodeState Evaluate(Context context)
    {
        float timerEnd = context.Get<float>(timerEndName);

        NodeState result = NodeState.Failure;

        if (Time.time < timerEnd)
        {
            result = node.Evaluate(context);
        }

        SetNodeState(context, result);
        return result;
    }
}