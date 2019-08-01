using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testing : Brain
{
    [Header("Behaviour")]
    public Patrol patrol = new Patrol();

    [Header("Context")]
    public ContextName patrolName;

    void Start()
    {
        patrol.agent = GetComponent<NavMeshAgent>();
        context.Set<Patrol>(patrolName, patrol);
    }

    private void Update()
    {
        rootNode.Evaluate(context);
    }
}
