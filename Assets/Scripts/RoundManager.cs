using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    RoundEnd,
    RoundPlay,
    Pause
}

/// <summary>
/// Round start/end, holds scores, max time, and gameState
/// </summary>
public class RoundManager : MonoBehaviour
{
    public GameStates gameState; //change to enum later
    [SerializeField]
    int roundTime = 180;

    public Dictionary<int,int> player_scores = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };

    /// <summary>
    /// starts timer
    /// </summary>
    void Start()
    {
        gameState = GameStates.RoundPlay;
        GetComponent<TimerBehavior>().StartTimer(roundTime);
    }

    /// <summary>
    /// 
    /// </summary>
    public void EndRound()
    {
        gameState = GameStates.RoundEnd;
        FindObjectOfType<ScoreDisplay>().DisplayScores(player_scores);
    }
}
