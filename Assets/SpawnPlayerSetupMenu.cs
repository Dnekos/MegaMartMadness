﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playersetupmenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("BigLayout");
        if(rootMenu != null)
        {
            var menu = Instantiate(playersetupmenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<SetupMenuController>().SetPlayerindex(input.playerIndex);
        }
    }
}