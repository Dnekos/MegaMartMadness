using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public PlayerInput input;

    private void Awake()
    {
        SpawnPlayerMenu(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += SpawnPlayerMenu;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= SpawnPlayerMenu;
    }

    void SpawnPlayerMenu(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "PlayerSelect")
        {
            Debug.Log("what");
            input.SwitchCurrentActionMap("Menus");

            var rootMenu = GameObject.Find("BigLayout");
            if (rootMenu != null)
            {
                Debug.Log(input.playerIndex);

                var menu = rootMenu.GetComponentsInChildren<SetupMenuController>().First(x => x.PlayerIndex == input.playerIndex);
                input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
                menu.Activate();
            }
        }
    }
}
