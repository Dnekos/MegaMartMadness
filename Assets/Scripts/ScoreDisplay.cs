using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class ScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// displays a txtbox with each player and their score, ordered greatest to least
    /// </summary>
    /// <param name="scores">dictionary of players indexes and scores</param>
    public void DisplayScores(Dictionary<int,int> scores)
    {
        //organizes list into ordered version
        Text[] ranks = GetComponentsInChildren<Text>();
        var ranked_scores = scores.ToList();
        ranked_scores.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));//
        
        for (int i = 0; i < 4; i++)
        {
            ranks[i].text = "Player " + (ranked_scores[i].Key+1) + ": " + ranked_scores[i].Value;
        }
    }
}