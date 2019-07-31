// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public abstract class Decorator : Node
{
    /* Child node to evaluate */
    public Node m_node;

    public Node node
    {
        get { return m_node; }
    }

    public Decorator(Node node)
    {
        m_node = node;
    }
}