using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public string gameState; //change to enum later
    [SerializeField]
    int roundTime = 180;

    public Dictionary<int,int> player_scores = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TimerBehavior>().StartTimer(roundTime);
    }

    public void EndRound()
    {
        gameState = "Round_End";
        FindObjectOfType<ScoreDisplay>().DisplayScores(player_scores);
    }
}
