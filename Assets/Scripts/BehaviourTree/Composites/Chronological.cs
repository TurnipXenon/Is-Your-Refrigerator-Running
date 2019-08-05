using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
[CreateAssetMenu(menuName ="BehaviourTree/Composite/Chronological")]
public class Chronological : Composite
{
    /** Must provide an initial set of children nodes to work */
    public Chronological(List<Node> nodes) : base(nodes) { }

    /* If any child node returns a failure, the entire node fails. Whence all  
     * nodes return a success, the node reports a success.
       If one node returns running, return running */
    public override NodeState Evaluate(Context context)
    {
        foreach (Node node in nodeList)
        {
            switch (node.Evaluate(context))
            {
                case NodeState.Failure:
                default:
                    SetNodeState(context, NodeState.Failure);
                    return NodeState.Failure;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    SetNodeState(context, NodeState.Running);
                    return NodeState.Running;
            }
        }

        SetNodeState(context, NodeState.Success);
        return NodeState.Success;
    }
}