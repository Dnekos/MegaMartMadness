using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public enum Stages
{
    StageOne = 1,
    StageTwo,
    StageThree,
    StageFour
}

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    int maxPlayers = 4;

    [SerializeField]
    Stages NextStage;

    [SerializeField]
    GameObject LevelMenu;
    [SerializeField]
    MultiplayerEventSystem P1Event;


    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)//error checking
            Debug.Log("trying to make new instances");
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
            //SceneManager.LoadScene((int)NextStage);
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