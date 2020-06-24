using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenuController : MonoBehaviour
{
    private int PlayerIndex;
    [SerializeField]
    Text titleText;
    [SerializeField]
    Button readyButton;

    public void SetPlayerindex(int pi)
    {
        PlayerIndex = pi;
        titleText.text = "Player " + (pi + 1);
    }
    public void ReadyPlayer()
    {
        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
