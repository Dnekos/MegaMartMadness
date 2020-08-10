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

    float ignoreImputtime = .3f;
    bool InputEnabled = false;
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
    //void ChangeSelection()
    //{
        /*
        //this whole thing calls the .db file to get the item index. look to StoreItem for an explanation
        string connection = "URI=file:" + Application.dataPath + "/" + "MegMart.db";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = "SELECT * FROM ItemDrops WHERE Catagory = '" + catagory + "' AND Rarity >= 0";//gets only first one
        Debug.Log("SELECT * FROM ItemDrops WHERE Catagory = " + catagory + " AND Rarity >= 0");
        //cmnd_read.CommandText = "SELECT * FROM ItemDrops WHERE Catagory = '" +catagory+ "' AND Rarity >= "+Random.Range(0f,1f);
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            shelves[shelf_index].FillShelf(int.Parse(reader[0].ToString()));
            break;
        }
        dbcon.Close();
    */
  //  }
    public void ChangeSelection(int levelID)
    {
        //this whole thing calls the .db file to get the item index. look to StoreItem for an explanation
        string connection = "URI=file:" + Application.dataPath + "/" + "MegMart.db";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = "SELECT * FROM LevelDescriptions WHERE ID = "+levelID;//gets only first one
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

   public void LoadLevel(int levelid)
    {
        if (!InputEnabled)
            return;

        if (levelid == -1)
            levelid = Random.Range(1, 5);
        SceneManager.LoadScene(levelid);
    }
}
