﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private const string TAG_CHARACTER = "Character";

    public Team team;

    public Mode gameplayMode;

    public GameState gameState;

    private void Start()
    {
        if (team == null)
        {
            Debug.LogWarning("No team assigned for " + name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameState.mode == gameplayMode && collision.gameObject.CompareTag(TAG_CHARACTER))
        {
            CharacterManager characterManager = collision.gameObject.GetComponent<CharacterManager>();
            if (characterManager)
            {
                if (characterManager.team == this.team)
                {
                    characterManager.Refresh();
                }
                else
                {
                    gameState.SetWinner(characterManager.team);
                }
            }
            else
            {
                Debug.LogWarning("No character manager found in a character");
            }
        }
    }
}