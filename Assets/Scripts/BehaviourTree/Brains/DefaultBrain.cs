using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DefaultBrain : Brain
{
    private const string TAG_CHARACTER = "Character";

    [Header("Variables")]
    public float dangerSqrMagnitude = 25f;
    public GameObject enemyElements;
    public float patientAITimer = 10f;

    [Header("Contexts")]
    public Transform target;
    public Transform retreatLocation;

    [Header("Context Names")]
    public ContextName navMeshAgentName;
    public ContextName locationName;
    public ContextName userTransformName;
    public ContextName enemyListName;
    public ContextName dangerSqrMagnitudeName;
    public ContextName retreatLocationName;
    public ContextName timerEndName;

    private new void Start()
    {
        base.Start();

        List<Transform> enemyList = new List<Transform>();
        foreach (Transform child in enemyElements.GetComponentInChildren<Transform>())
        {
            if (child.CompareTag(TAG_CHARACTER))
            {
                enemyList.Add(child);
            }
        }

        context.Set<NavMeshAgent>(navMeshAgentName, GetComponent<NavMeshAgent>());
        context.Set<Transform>(locationName, target);
        context.Set<Transform>(userTransformName, transform);
        context.Set<List<Transform>>(enemyListName, enemyList);
        context.Set<float>(dangerSqrMagnitudeName, dangerSqrMagnitude);
        context.Set<Transform>(retreatLocationName, retreatLocation);
        context.Set<float>(timerEndName, Time.time + patientAITimer);
    }

    private new void Update()
    {
        base.Update();
    }
}
