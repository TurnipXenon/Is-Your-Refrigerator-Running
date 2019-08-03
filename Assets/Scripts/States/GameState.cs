using RoboRyanTron.Unite2017.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/States/Game State")]
public class GameState : ScriptableObject
{
    public float freshnessDuration;

    public GameEvent onLevelEnd;

    [Header("Modes")]
    public Mode gameplay;
    public Mode gameResult;

    [HideInInspector]
    public Team winningTeam;
    [HideInInspector]
    public Mode mode;

    public void SetWinner(Team team)
    {
        if (this.winningTeam == null)
        {
            this.winningTeam = team;
            OnLevelEnd();
            onLevelEnd.Raise();
        }
    }

    #region Game Event Tied
    public void OnLevelStart()
    {
        mode = gameplay;
        winningTeam = null;
    }

    public void OnLevelEnd()
    {
        mode = gameResult;
    }
    #endregion
}
