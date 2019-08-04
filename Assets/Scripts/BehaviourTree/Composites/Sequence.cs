﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public class Sequence : Composite
{
    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes) : base(nodes) { }

    private bool anyChildRunning;

    /* If any child node returns a failure, the entire node fails. Whence all  
     * nodes return a success, the node reports a success. */
    public override NodeState Evaluate(Context context)
    {
        anyChildRunning = false;

        foreach (Node node in nodeList)
        {
            switch (node.Evaluate(context))
            {
                case NodeState.Failure:
                    SetNodeState(context, NodeState.Failure);
                    return nodeState;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    return NodeState.Success;
            }
        }

        if (anyChildRunning)
        {
            SetNodeState(context, NodeState.Running);
        }
        else {
            SetNodeState(context, NodeState.Success);
        }

        return nodeState;
    }
}