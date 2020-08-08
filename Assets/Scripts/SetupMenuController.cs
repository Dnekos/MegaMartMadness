using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenuController : MonoBehaviour
{
    public int PlayerIndex;
    [SerializeField]
    Text titleText;
    [SerializeField]
    Button readyButton;

    float ignoreImputtime = 1.5f;
    bool InputEnabled = false;

    public void Activate()
    {
        GetComponentInChildren<RectMask2D>().gameObject.SetActive(false);
        ignoreImputtime = Time.time + ignoreImputtime;
    }
    private void Update()
    {
        if (Time.time > ignoreImputtime && ignoreImputtime != 1.5f)
            InputEnabled = true;
    }


    public void ReadyPlayer()
    {
        if (!InputEnabled)
            return;

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
 
    public void DisconnectPlayer()
    {
        if (!InputEnabled)
            return;

        Debug.Log(PlayerIndex);
        PlayerConfigManager.Instance.DisconnectPlayer(PlayerIndex);
    }
}
