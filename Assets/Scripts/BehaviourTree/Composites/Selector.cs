using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
[CreateAssetMenu(menuName = "BehaviourTree/Composite/Selector")]
public class Selector : Composite
{
    public Selector(List<Node> nodes) : base(nodes) { }

    /* If any of the children reports a success, the selector will 
     * immediately report a success upwards. If all children fail, 
     * it will report a failure instead.*/
    public override NodeState Evaluate(Context context)
    {
        foreach (Node node in nodeList)
        {
            switch (node.Evaluate(context))
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    SetNodeState(context, NodeState.Success);
                    return nodeState;
                case NodeState.Running:
                    SetNodeState(context, NodeState.Running);
                    return nodeState;
                default:
                    continue;
            }
        }

        return nodeState;
    }
}