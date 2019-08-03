using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI textResult;

    public GameState gameState;

    #region Events
    public void OnLevelStart()
    {
        textResult.gameObject.SetActive(false);
    }

    public void OnLevelEnd()
    {
        textResult.text = gameState.winningTeam.teamName + " wins!";
        textResult.gameObject.SetActive(true);
    }
    #endregion
}
