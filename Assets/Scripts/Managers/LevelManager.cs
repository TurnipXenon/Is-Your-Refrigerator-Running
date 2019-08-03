using RoboRyanTron.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameState gameState;

    public GameEvent levelStart;

    private void Start()
    {
        gameState.OnLevelStart();
        levelStart.Raise();
    }
}
