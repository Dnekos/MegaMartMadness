using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour
{
    public float timeRemaining = 10f;
    public bool timerIsRunning = false;
    [SerializeField]
    Text timeText;
    
    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            DisplayTime();

            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                Debug.Log("Time has run out!");
                timerIsRunning = false;
                GetComponent<RoundManager>().EndRound();
            }
        }
        else
            timeText.text = "";
    }

    public void StartTimer(int seconds)
    {
        timeRemaining = seconds;
        timerIsRunning = true;
    }

    /// <summary>
    /// formats and displays time into minute:second format
    /// </summary>
    /// <param name="timeToDisplay">number in seconds to be formatted</param>
    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        if (timeText != null)
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
