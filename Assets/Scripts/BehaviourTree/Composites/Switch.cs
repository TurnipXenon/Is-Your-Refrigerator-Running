using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
[CreateAssetMenu(menuName = "BehaviourTree/Composite/Switch")]
public class Switch : Composite
{
    public Switch(List<Node> nodes) : base(nodes) { }

    /* If any of the children reports a success or running, the switch will 
     * immediately report a success or running upwards. If all children fail, 
     * it will report a failure instead.*/
    public override NodeState Evaluate(Context context)
    {
        NodeState nodeState = NodeState.Failure;

        foreach (Node node in nodeList)
        {
            nodeState = node.Evaluate(context);
            switch (nodeState)
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success: case NodeState.Running:
                    SetNodeState(context, nodeState);
                    return nodeState;
                default:
                    SetNodeState(context, NodeState.Failure);
                    return NodeState.Failure;
            }
        }

        SetNodeState(context, nodeState);
        return nodeState;
    }
}