using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField]
    Image LvlImage;
    [SerializeField]
    Text LvlTitle;
    [SerializeField]
    Text LvlDesc;

    [SerializeField]
    MultiplayerEventSystem MenuEvents;
    int lastselectedID;
    int currentselectedID;

    float ignoreImputtime = .3f;//timer is used to prevent player accidently clicking button during menu transition
    bool InputEnabled = false;

    /// <summary>
    /// starts input timer
    /// </summary>
    private void OnEnable()
    {
        ignoreImputtime = Time.time + ignoreImputtime;
    }

    private void Start()
    {
        GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>().LevelMenu = gameObject;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time > ignoreImputtime && ignoreImputtime != 1.5f)
            InputEnabled = true;
    }

    /// <summary>
    /// grabs the name/description from .db and displays it
    /// </summary>
    /// <param name="levelID"></param>
    public void ChangeSelection(int levelID)
    {
        //this whole thing calls the .db file to get the level descriptions. look to StoreItem for an explanation
        string connection = "URI=file:" + Application.dataPath + "/" + "MegMart.db";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = "SELECT * FROM LevelDescriptions WHERE ID = " + levelID;//gets only first one
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            LvlTitle.text = reader[1].ToString();
            LvlDesc.text = reader[2].ToString();
            LvlImage.sprite = Resources.Load<Sprite>(reader[3].ToString());
            break;
        }
        dbcon.Close();
    }


    /// <summary>
    /// loads the level based on the button's input value
    /// </summary>
    /// <param name="levelid"></param>
    public void LoadLevel(int levelid)
    {
        if (!InputEnabled)
            return;

        if (levelid == -1) //for random button
            levelid = Random.Range(2, 6);
        SceneManager.LoadScene(levelid);
    }
}
