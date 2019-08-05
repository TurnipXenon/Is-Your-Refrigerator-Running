using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private Hashtable hashtable = new Hashtable();
    public Node rootNode;

#if UNITY_EDITOR
    public Dictionary<int, NodeState> nodeStateDict = new Dictionary<int, NodeState>();
#endif

    public Context(Node rootNode) { }

    public void Set<T>(ContextName contextName, T value)
    {
        hashtable[contextName] = value;
    }

    public void Remove(ContextName keyName)
    {
        if (hashtable.Contains(keyName))
        {
            hashtable.Remove(keyName);
        }
    }

    public bool Exists(ContextName keyName)
    {
        return hashtable.Contains(keyName);
    }

    public T Get<T>(ContextName contextName)
    {
        System.Object returnObject = hashtable[contextName];
        if (returnObject is T)
        {
            return (T)returnObject;
        }
        else if (returnObject == null)
        {
            Debug.LogWarning(contextName.ToString() + " not found in Context");
        }
        else
        {
            Debug.LogWarning(contextName.ToString() + " has a different type from what's being asked");
        }
        return default;
    }

    public T Get<T>(ContextName keyName, T defaultValueName)
    {
        System.Object returnObject = hashtable[keyName];
        if (returnObject is T)
        {
            return (T)returnObject;
        }
        else if (returnObject != null)
        {
            Debug.LogWarning(keyName.ToString() + " has a different type from what's being asked");
        }
        return defaultValueName;
    }
}