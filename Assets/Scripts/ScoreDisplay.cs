using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject Continue;

    /// <summary>
    /// displays a txtbox with each player and their score, ordered greatest to least
    /// </summary>
    /// <param name="scores">dictionary of players indexes and scores</param>
    public void DisplayScores(Dictionary<int,int> scores)
    {
        Continue.SetActive(true);

        //organizes list into ordered version
        Text[] ranks = GetComponentsInChildren<Text>();
        var ranked_scores = scores.ToList();
        ranked_scores.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));//
        ranked_scores.Reverse();

        //increase wins for top scoring players (draws are wins for all who drew)
        var wins = PlayerConfigManager.Instance.PlayerWins;
        int j = 0;
        do
        {
            wins[ranked_scores[j].Key]++;
            Debug.Log(ranked_scores[j].Key + " " + wins[ranked_scores[j].Key] + " wins");
            j++;
        } while (j != wins.Length && ranked_scores[j].Value == ranked_scores[0].Value);
        

        for (int i = 0; i < 4; i++)
        {
            ranks[i].text = "Player " + (ranked_scores[i].Key+1) + ": " + ranked_scores[i].Value;
        }
    }
}