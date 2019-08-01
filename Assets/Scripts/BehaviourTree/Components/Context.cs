using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    private Hashtable hashtable = new Hashtable();

    public Context() { }

    public void Set<T>(ContextName contextName, T value)
    {
        hashtable[contextName] = value;
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
}