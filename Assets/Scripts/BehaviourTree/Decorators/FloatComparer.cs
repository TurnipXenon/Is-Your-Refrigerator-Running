﻿
using UnityEngine;
/**
*  Does the given node if result matches wanted result
*/
[CreateAssetMenu(menuName = "BehaviourTree/Decorator/Float Comparer")]
public class FloatComparer : Decorator
{
    [Header("Receiving")]
    public ContextName valueName;
    public ContextName toBeComparedName;
    public Result desiredResult;

    public enum Result
    {
        Approximately,
        LessThan,
        GreaterThan
    }

    public override NodeState Evaluate(Context context)
    {
        float value = context.Get<float>(valueName);
        float toBeCompared = context.Get<float>(toBeComparedName);
        NodeState result = NodeState.Failure;

        if (Mathf.Approximately(value, toBeCompared))
        {
            if (desiredResult == Result.Approximately)
            {
                result = NodeState.Success;
            }
        }
        else if (value < toBeCompared)
        {
            if (desiredResult == Result.LessThan)
            {
                result = NodeState.Success;
            }
        }
        else
        {
            if (desiredResult == Result.GreaterThan)
            {
                result = NodeState.Success;
            }
        }

        if (result == NodeState.Success)
        {
            result = node.Evaluate(context);
        }

        SetNodeState(context, result);
        return result;
    }
}