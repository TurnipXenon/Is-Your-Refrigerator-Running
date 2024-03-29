﻿// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

public abstract class Node : ScriptableObject
{
#if UNITY_EDITOR
    [TextArea]
    public string developerDescription;
#endif

    /* The constructor for the node */
    public Node() { }

    /* Implementing classes use this method to evaluate the desired set of conditions */
    public abstract NodeState Evaluate(Context context);

    protected void SetNodeState(Context context, NodeState nodeState)
    {
#if UNITY_EDITOR
        if (this == context.rootNode)
        {
            context.nodeStateDict.Clear();
        }

        context.nodeStateDict[this.GetInstanceID()] = nodeState;
#endif
    }
}