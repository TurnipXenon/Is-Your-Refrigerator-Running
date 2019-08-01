using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public Context context;
    public Node rootNode;

    public void OnEnable()
    {
        context = new Context(rootNode);
    }
}
