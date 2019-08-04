using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public Context context;
    public Node rootNode;

    #region Callbacks
    public void OnEnable()
    {
        context = new Context(rootNode);
    }

    public void Start()
    {
        if (context == null)
        {
            Debug.LogWarning("No context for brain: " + name);
        }

        if (rootNode == null)
        {
            Debug.LogWarning("No root node for brain: " + name);
        }
    }

    public void Update()
    {
        rootNode?.Evaluate(context);
    }
    #endregion
}
