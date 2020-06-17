using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class ScoreDisplay : MonoBehaviour
{
    public void DisplayScores(Dictionary<int,int> scores)
    {
        //int[] ranked_scores = { 0, 0, 0, 0 };
       // Array.Copy(scores,ranked_scores,4);
       // Array.Sort(ranked_scores);
        Text[] ranks = GetComponentsInChildren<Text>();

        var ranked_scores = scores.ToList();

        ranked_scores.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));



        for (int i = 0; i < 4; i++)
        {
            ranks[i].text = "Player " + (ranked_scores[i].Key+1) + ": " + ranked_scores[i].Value;
        }
    }
}
