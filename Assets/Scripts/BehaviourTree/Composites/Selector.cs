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
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate(context))
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    m_nodeState = NodeState.Success;
                    return m_nodeState;
                case NodeState.Running:
                    m_nodeState = NodeState.Running;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = NodeState.Failure;
        return m_nodeState;
    }

    public void Output()
    {
        Debug.Log("Output size: " + m_nodes.Count.ToString());
    }
}