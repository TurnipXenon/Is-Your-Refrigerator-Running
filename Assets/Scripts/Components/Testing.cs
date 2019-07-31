using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testing : MonoBehaviour
{
    [Header("Behaviour")]
    public Patrol patrol = new Patrol();

    public PatrolDecorator patrolAI;

    [Header("Context")]
    public ContextName patrolName;

    private Context context;

    void Start()
    {
        patrol.agent = GetComponent<NavMeshAgent>();

        context = new Context();
        context.Set<Patrol>(patrolName, patrol);
    }

    private void Update()
    {
        patrolAI.Evaluate(context);
    }
}
