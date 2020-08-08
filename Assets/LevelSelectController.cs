using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField]
    MultiplayerEventSystem MenuEvents;
    int lastselectedID;
    int currentselectedID;


    float ignoreImputtime = .3f;
    bool InputEnabled = false;

    private void Awake()
    {
        ignoreImputtime = Time.time + ignoreImputtime;
    }
    private void Update()
    {
        if (Time.time > ignoreImputtime && ignoreImputtime != 1.5f)
            InputEnabled = true;

        currentselectedID = MenuEvents.currentSelectedGameObject.GetInstanceID();
        if (currentselectedID != lastselectedID)//basically a workaround to check if the player did an input
            ChangeSelection();
        lastselectedID = currentselectedID;
    }
    void ChangeSelection()
    {
        //MenuEvents
    }

   public void LoadLevel(int levelid)
    {
        if (!InputEnabled)
            return;

        if (levelid == -1)
            levelid = Random.Range(1, 5);
        SceneManager.LoadScene(levelid);
    }
}
