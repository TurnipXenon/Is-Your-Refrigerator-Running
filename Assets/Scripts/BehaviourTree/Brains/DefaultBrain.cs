using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DefaultBrain : Brain
{
    [Header("Contexts")]
    public Transform target;

    [Header("Context Names")]
    public ContextName navMeshAgentName;
    public ContextName locationName;

    private new void Start()
    {
        base.Start();

        context.Set<NavMeshAgent>(navMeshAgentName, GetComponent<NavMeshAgent>());
        context.Set<Transform>(locationName, target);
    }
}
