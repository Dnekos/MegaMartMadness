using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public string gameState; //change to enum later
    [SerializeField]
    int roundTime = 180;

    public int[] player_scores = { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TimerBehavior>().StartTimer(roundTime);
    }

    public void EndRound()
    {
        gameState = "Round_End";
    }
}
