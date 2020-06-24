using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    int maxPlayers = 4;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("trying to make new instances");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void ReadyPlayer (int index)
    {
        playerConfigs[index].isReady = true;
        if(playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
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