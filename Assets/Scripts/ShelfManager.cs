using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public enum ShelveType//for item spawning
{
    Common,
    Uncommon,
    Rare,
    Power_Ups
}

/// <summary>
/// this is used on a GameObject that either has or has a child with ItemDispenser, which it refills when RoundManager tells it to
/// </summary>
public class ShelfManager : MonoBehaviour
{
    [SerializeField]
    ShelveType catagory;
    ItemDispenser[] shelves;

    // Start is called before the first frame update
    void Start()
    {
        shelves = GetComponentsInChildren<ItemDispenser>();
    }

    public void StockShelves(int maxShelves)
    {
        //reset item_index so that I know when i set a shelf to be stocked
        for (int i = 0; i < shelves.Length;i++)
        {
            shelves[i].item_index = 0;
        }


        int shelves_tbs = Random.Range(1, Mathf.Clamp(maxShelves,1,shelves.Length));//how many times (usually 1)

        for (int i = 0; i < shelves_tbs; i++)//once for each shelf to be stocked
        {
            //this bit sets the index to a shelf that hasnt been stocked this wave
            int shelf_index = 0;
            do
                shelf_index = Random.Range(0, shelves.Length);
            while (shelves[shelf_index].item_index != 0);


            //this whole thing calls the .db file to get the item index. look to StoreItem for an explanation
            string connection = "URI=file:" + Application.dataPath + "/" + "MegMart.db";
            IDbConnection dbcon = new SqliteConnection(connection);
            dbcon.Open();
            IDbCommand cmnd_read = dbcon.CreateCommand();
            IDataReader reader;
            cmnd_read.CommandText = "SELECT * FROM ItemDrops WHERE Catagory = '" +catagory+ "' AND Rarity >= "+Random.Range(0f,1f);
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                shelves[shelf_index].FillShelf(int.Parse(reader[0].ToString()));
            }
            dbcon.Close();
        }
    }
}
