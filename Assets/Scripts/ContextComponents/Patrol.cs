using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** Always set agent!!! */
[System.Serializable]
public class Patrol
{
    public Transform waypointList;

    [System.NonSerialized]
    public int currentIndex = 0;

    private NavMeshAgent _agent;

    public NavMeshAgent agent
    {
        set { _agent = value; }
        get
        {
            if (_agent == null)
            {
                Debug.LogWarning("Set NavMeshAgent during OnStart()");
            }
            return _agent;
        }
    }
}
