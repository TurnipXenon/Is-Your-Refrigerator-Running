using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : MonoBehaviour
{
    private const string TAG_CHARACTER = "Character";
    private const float TIME_NOT_SET = -1f;
    private const float TIME_NOT_SET_COMPARISON = -0.5f;

    [Header("Properties")]
    public Team team;

    [Header("UI")]
    public UIBar uiBar;

    [Header("Components")]
    public GameObject parent;

    [Header("States")]
    public GameState gameState;
    public Mode gameplayMode;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_CHARACTER))
        {
            CharacterManager characterManager = other.gameObject.GetComponentInChildren<CharacterManager>();
            if (characterManager)
            {
                if (characterManager.team != this.team)
                {
                    OnCharactersHit(characterManager);
                }
            }
            else
            {
                Debug.LogWarning("No character manager found in a character");
            }
        }
    }

    private void OnCharactersHit(CharacterManager other)
    {
        if (Mathf.Approximately(this.freshnessEndTime, other.freshnessEndTime))
        {
            other.DestroyCharacter();
            this.DestroyCharacter();
        }
        else if (this.freshnessEndTime > other.freshnessEndTime)
        {
            other.DestroyCharacter();
        }
        else
        {
            this.DestroyCharacter();
        }
    }

    private void DestroyCharacter()
    {
        Destroy(parent);
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