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

    /// <summary>
    /// sets win star counter
    /// </summary>
    private void Awake()
    {
        Winstxt.text = PlayerConfigManager.Instance.PlayerWins[PlayerIndex].ToString();
    }

    /// <summary>
    /// turns of image cover and sets buffer time
    /// </summary>
    public void Activate()
    {
        CPUCover.SetActive(false);
        ignoreImputtime = Time.time + ignoreImputtime;
    }

    /// <summary>
    /// handles a buffer so that the player accidently press a button when connecting the controller
    /// </summary>
    private void Update()
    {
        if (Time.time > ignoreImputtime && ignoreImputtime != 1.5f)
            InputEnabled = true;
    }

    /// <summary>
    /// calls the ReadyPlayer function
    /// </summary>
    public void ReadyPlayer()
    {
        if (!InputEnabled)
            return;

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
 
    /// <summary>
    /// calls the DisconnectPlayer function
    /// </summary>
    public void DisconnectPlayer()
    {
        if (!InputEnabled)
            return;
        
        CPUCover.gameObject.SetActive(true);
        Debug.Log("Player "+ PlayerIndex + " disconnected");
        PlayerConfigManager.Instance.DisconnectPlayer(PlayerIndex);
    }
}
