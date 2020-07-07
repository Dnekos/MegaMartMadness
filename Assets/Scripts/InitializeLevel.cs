﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject playerPrefab;



    /// <summary>
    /// spawns in the player objects and starts the process of initializing them
    /// </summary>
    void Start()
    {
        var PlayerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < PlayerConfigs.Length;i++)
        {
            var player = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation, gameObject.transform);
            player.GetComponent<InputHandler>().InitializePlayer(PlayerConfigs[i]);
            if (PlayerConfigs.Length > 2)
            {
                if (i < 3)
                    player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * 0.5f, 0.5f, 0.5f, 0.5f);
                else
                    player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * 0.5f, 0, 0.5f, 1);
            }
            else if (PlayerConfigs.Length == 2)
            {
                player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * .5f, 0, 0.5f, 1);
            }
        }
    }
}
