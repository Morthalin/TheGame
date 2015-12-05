using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class Inventory
{
    public int playerID;
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
        SqliteConnection.ClearAllPools();
        playerID = ID;
    }

	public void Save()
	{
		Loot l = GameObject.Find ("Player").GetComponent<Loot> ();
		if (!l) {
			Debug.LogError("Loot obj has not been loaded as player component.");
			return;
		}
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();

		foreach (var item in l.itemsQuantity.Keys) {
			for (int i = 0; i < l.itemsQuantity[item]; i++) {
				string sqlQuery = "insert into Inventory (playerID,itemID) values ("+playerID+","+item+");";
				command.CommandText = sqlQuery;
				command.ExecuteReader();
			}
		}
		
		command.Dispose();
		connection.Close();
		SqliteConnection.ClearAllPools();
	}
}
