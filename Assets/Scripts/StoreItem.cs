using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class StoreItem
{
    public Sprite image;
    public int index;
    public int point_value;
    public string group;

    /// <summary>
    /// contstructor that takes in item index and gets data from .db file
    /// </summary>
    /// <param name="i">Item Index</param>
    public StoreItem(int i)
    {
        // Open connection
        string connection = "URI=file:" + Application.dataPath + "/" + "MegMart.db";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        
        // Read and print all values in table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = "SELECT * FROM ItemDrops WHERE ID = " + i;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            //set values
            index = int.Parse(reader[0].ToString());
            group = reader[2].ToString();
            point_value = int.Parse(reader[4].ToString());
            image = Resources.Load<Sprite>(reader[1].ToString());
            if(image == null)
                Resources.Load<Sprite>("Food_Canned");
        }

        // Close connection
        dbcon.Close();
    }

    /// <summary>
    /// copy constructor
    /// </summary>
    /// <param name="item"></param>
    public StoreItem(StoreItem item)
    {
        index = item.index;
        image = item.image;
        point_value = item.point_value;
        group = item.group;
    }
}
