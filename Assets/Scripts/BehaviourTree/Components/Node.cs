// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
using UnityEngine;

public abstract class Node : ScriptableObject
{
#if UNITY_EDITOR
    public float NodeHeight = 50f;
    public float NodeWidth = 200f;
    public Vector2 DistanceFromRoot = new Vector2(300f, 0f);
#endif

    /* Delegate that returns the state of the node.*/
    public delegate NodeState NodeReturn();

    /* The current state of the node */
    protected NodeState m_nodeState;
    protected Context context;

    public NodeState nodeState
    {
        get { return m_nodeState; }
    }

    /* The constructor for the node */
    public Node() { }

    /* Implementing classes use this method to evaluate the desired set of conditions */
    public abstract NodeState Evaluate(Context context);
}