// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public abstract class Decorator : Node
{
    /* Child node to evaluate */
    public Node node;

    public Decorator(Node node)
    {
        node = node;
    }
}