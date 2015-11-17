using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class Inventory
{
    private int playerID;

    public int PlayerID {get; set; }
    public List<int> itemsID = new List<int>();

    public Inventory(int ID)
    {
        //nacitanie dat z DB
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;

        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM Inventory WHERE playerID = " + ID + ";";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            itemsID.Add(reader.GetInt32(2));
        }
        reader.Close();
        command.Dispose();
        connection.Close();

        playerID = ID;
    }
}
