using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    int maxPlayers = 4;
    [HideInInspector]
    public GameObject LevelMenu;
    public int[] PlayerWins = new int[4];

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)//error checking
        {
            if (Instance != this)//deletes the new version of PlayerConfigManager when returning to PLayerSelect after the first time
            { //TODO: MAKE PLAYERCONFIGMANAGER HAPPEN IN PREVIOUS SCREEN SO THIS IS NOT NEEDED
                Debug.Log("deleting new instance of PCM");
                Destroy(gameObject);
            }
        }
        else
        {
            //this allows future scenes to utilize the controllers that are already in 
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    /// <summary>
    /// returns the list
    /// </summary>
    /// <returns></returns>
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    /// <summary>
    /// sets up
    /// </summary>
    /// <param name="index">Player's list index</param>
    public void ReadyPlayer (int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady == true))
        {
            LevelMenu.SetActive(true);

            //assigns Player 1 to the level select menu
            MultiplayerEventSystem P1Event = GameObject.Find("SetupPanel").GetComponent<MultiplayerEventSystem>();
            P1Event.playerRoot = LevelMenu;
            P1Event.SetSelectedGameObject(LevelMenu.GetComponentInChildren<Button>().gameObject);

            foreach (MultiplayerEventSystem player in GameObject.FindObjectsOfType<MultiplayerEventSystem>())
            {
                if (player.playerRoot == LevelMenu)
                    continue;//skips Player 1
                player.SetSelectedGameObject(null); //makes it so that other players can't disconnect during Level Selection
            }
        }
    }

    /// <summary>
    /// deactivates user then removes user from the config list
    /// </summary>
    /// <param name="index">Player's list index</param>
    public void DisconnectPlayer(int index)
    {
        Debug.Log(playerConfigs[index].isReady);
        playerConfigs[index].theInput.user.UnpairDevicesAndRemoveUser();
        playerConfigs[index].theInput.DeactivateInput();
        Destroy(playerConfigs[index].theInput.gameObject);


        playerConfigs.RemoveAt(index);
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player " + pi.playerIndex + " Joined");
        if(!playerConfigs.Any(p=>p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}
public class PlayerConfiguration
{

    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        theInput = pi;
    }
    public PlayerInput theInput { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
}