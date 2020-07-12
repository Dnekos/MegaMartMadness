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
    public GameStates gameState; 
    [SerializeField]
    int roundTime = 180;

    public Dictionary<int,int> player_scores = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };

    //stocking variables
    [SerializeField]
    float timeToStock = 30;
    float maxTTS = 30;
    ShelfManager[] shelves;

    /// <summary>
    /// starts timer
    /// </summary>
    void Start()
    {
        gameState = GameStates.RoundPlay;
        GetComponent<TimerBehavior>().StartTimer(roundTime);
        shelves = FindObjectsOfType<ShelfManager>();
    }

    private void Update()
    {
        timeToStock -= Time.deltaTime;
        if (timeToStock <= 0)
        {
            Debug.Log("stocking now");

            timeToStock = maxTTS;
            for (int i = 0; i < shelves.Length; i++)
                shelves[i].StockShelves(1);
        }
    }

    /// <summary>
    /// changes gamestate and shows scores
    /// </summary>
    public void EndRound()
    {
        gameState = GameStates.RoundEnd;
        FindObjectOfType<ScoreDisplay>().DisplayScores(player_scores);
    }
}
