using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Brain))]
public class CharacterManager : MonoBehaviour
{
    private const float TIME_NOT_SET = -1f;
    private const float TIME_NOT_SET_COMPARISON = -0.5f;

    [Header("Properties")]
    public Team team;

    [Header("UI")]
    public UIBar uiBar;

    [Header("States")]
    public GameState gameState;

    [HideInInspector]
    public float freshnessEndTime = TIME_NOT_SET;
    [HideInInspector]
    public float lastRefreshTime = TIME_NOT_SET;

    private void Start()
    {
        if (team == null)
        {
            Debug.LogWarning("No team assigned for " + name);
        }
    }

    private void Update()
    {
        if (freshnessEndTime > TIME_NOT_SET)
        {
            uiBar.UpdateBar(gameState.freshnessDuration - (Time.time - lastRefreshTime), gameState.freshnessDuration);
        }
    }

    public void Refresh()
    {
        lastRefreshTime = Time.time;
        freshnessEndTime = Time.time + gameState.freshnessDuration;
    }

    #region Events
    public void OnLevelStart()
    {
        Refresh();
    }
    #endregion
}