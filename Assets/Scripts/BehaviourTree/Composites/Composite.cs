using System.Collections.Generic;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public abstract class Composite : Node
{
    /** The child nodes for this selector */
    public List<Node> m_nodes = new List<Node>();


    /** The constructor requires a list of child nodes to be  
     * passed in*/
    public Composite(List<Node> nodes)
    {
        m_nodes = nodes;
    }
}