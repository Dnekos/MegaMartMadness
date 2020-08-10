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
            Debug.Log("multiple instances of PCM");
            if (Instance != this)
            {
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

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void ReadyPlayer (int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.All(p => p.isReady == true))
        {
            MultiplayerEventSystem P1Event = GameObject.Find("SetupPanel").GetComponent<MultiplayerEventSystem>();

            LevelMenu.SetActive(true);
            P1Event.playerRoot = LevelMenu;

            P1Event.SetSelectedGameObject(LevelMenu.GetComponentInChildren<Button>().gameObject);
            /*foreach (MultiplayerEventSystem player in GameObject.FindObjectsOfType<MultiplayerEventSystem>())
            {
                player.SetSelectedGameObject(LevelMenu.GetComponentInChildren<Button>().gameObject);
            }*/
        }
    }

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