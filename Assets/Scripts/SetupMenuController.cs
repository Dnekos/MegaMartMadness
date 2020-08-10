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
    [SerializeField]
    GameObject CPUCover;
    [SerializeField]
    Text Winstxt;

    float ignoreImputtime = 1.5f;
    bool InputEnabled = false;

    private void Awake()
    {
        Winstxt.text = PlayerConfigManager.Instance.PlayerWins[PlayerIndex].ToString();
    }

    public void Activate()
    {
        CPUCover.gameObject.SetActive(false);
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
        
        CPUCover.gameObject.SetActive(true);
        Debug.Log("Player "+ PlayerIndex + " disconnected");
        PlayerConfigManager.Instance.DisconnectPlayer(PlayerIndex);
    }
}
