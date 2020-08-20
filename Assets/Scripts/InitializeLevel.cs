using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject enemyPrefab;
    [Header("Debug")]
    [SerializeField]
    bool SpawnEnemies = true;

    /// <summary>
    /// spawns in the player objects and starts the process of initializing them
    /// </summary>
    void Start()
    {
        PlayerConfiguration[] PlayerConfigs = { };
        
        //if there are no players, go back to PlayerSelect (mainly here for debugging purposes)
        try { PlayerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray(); }
        catch { SceneManager.LoadScene("PlayerSelect"); }

        for (int i = 0; i < PlayerConfigs.Length;i++)
        {
            //spawns player object and calls InitializePlayer
            var player = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation, gameObject.transform);
            player.GetComponent<InputHandler>().InitializePlayer(PlayerConfigs[i]);
            player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_" + i);//sets player sprite;

            if (PlayerConfigs.Length > 2) //sets up camera orientation (splitscreen) based on amount of players
            {
                if (i < 3)
                    player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * 0.5f, 0.5f, 0.5f, 0.5f);
                else
                    player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * 0.5f, 0, 0.5f, 1);
            }
            else if (PlayerConfigs.Length == 2)
                player.GetComponentInChildren<Camera>().rect = new Rect((i % 2) * .5f, 0, 0.5f, 1);
        }
        if (SpawnEnemies)//debug if
            for (int i = 3; i > PlayerConfigs.Length - 1; i--)
            {
                enemyPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player_" + i);//sets player sprite;
                enemyPrefab.GetComponent<EnemyMovement>().pathtag = (1 << (i + 1));//pathtags are bitmaps, thus the wonk setting
                Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation, gameObject.transform);
            }
    }
}