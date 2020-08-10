using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
