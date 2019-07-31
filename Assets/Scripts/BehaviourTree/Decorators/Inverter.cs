// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public class Inverter : Decorator
{
    public Inverter(Node node) : base(node) { }

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */
    public override NodeState Evaluate(Context context)
    {
        switch (m_node.Evaluate(context))
        {
            case NodeState.Failure:
                m_nodeState = NodeState.Success;
                return m_nodeState;
            case NodeState.Success:
                m_nodeState = NodeState.Failure;
                return m_nodeState;
            case NodeState.Running:
                m_nodeState = NodeState.Running;
                return m_nodeState;
        }
        m_nodeState = NodeState.Success;
        return m_nodeState;
    }
}