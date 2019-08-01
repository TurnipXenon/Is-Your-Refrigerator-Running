using System.Collections.Generic;
using UnityEngine;

// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public abstract class Composite : Node
{
    [Header("Children")]
    /** The child nodes for this selector */
    public List<Node> nodeList = new List<Node>();


    /** The constructor requires a list of child nodes to be  
     * passed in*/
    public Composite(List<Node> nodes)
    {
        nodeList = nodes;
    }
}