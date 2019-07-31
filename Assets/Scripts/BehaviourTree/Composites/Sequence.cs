using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public class Sequence : Composite
{
    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes) : base(nodes) { }

    /* If any child node returns a failure, the entire node fails. Whence all  
     * nodes return a success, the node reports a success. */
    public override NodeState Evaluate(Context context)
    {
        bool anyChildRunning = false;

        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate(context))
            {
                case NodeState.Failure:
                    m_nodeState = NodeState.Failure;
                    return m_nodeState;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = NodeState.Success;
                    return m_nodeState;
            }
        }
        m_nodeState = anyChildRunning ? NodeState.Running : NodeState.Success;
        return m_nodeState;
    }
}