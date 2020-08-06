using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using System.Linq;


public class SpawnPlayerSetupMenu : MonoBehaviour
{
   // public GameObject playersetupmenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        Debug.Log("what");
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
