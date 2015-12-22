using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class Inventory
{
    public int playerID;
 //   public List<int> itemsID = new List<int>();
	public  Dictionary<int, int>           itemsQuantity = new Dictionary<int, int>();

	public List<string> equipmentOrder = new List<string> ();

	public Dictionary<string, BaseItemStats> equipmentMap  = new Dictionary<string, BaseItemStats> ();

	public BaseItemStats emptyItem = new BaseItemStats ();

	// k itemID aktivnimu itemu mapuje zvybajici cas.
	public Dictionary<int, float> 			activePotions = new Dictionary<int, float>();

    public Inventory(int ID)
    {
		// Nacte data itemu
		ItemsData.Load ();
		playerID = ID;
        // polozky z inventare
		loadInventoryItems ();
        // nacteni equipmentu z db
		loadEquipment ();
		// ulozene aktivni napoje
		loadActivePotions ();
    }

	public void Save()
	{
		storeInventoryItems ();
		storeEquipment ();
		storeActivePotions ();
	}
	
	void loadInventoryItems(){
		itemsQuantity.Clear ();
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM Inventory WHERE playerID = " + playerID + ";";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			itemsQuantity[reader.GetInt32(2)] = reader.GetInt32(3);
		}
		reader.Close();
		command.Dispose();
		connection.Close();
		SqliteConnection.ClearAllPools();
	}
	
	void initEquipment(){
		equipmentOrder.Clear();
		equipmentMap.Clear ();
		equipmentOrder.Add ("Hlava");
		equipmentOrder.Add ("Hruď");
		equipmentOrder.Add ("Ramena");
		equipmentOrder.Add ("Ruce");
		equipmentOrder.Add ("Nohy");
		equipmentOrder.Add ("Chodidla");
		
		equipmentOrder.Add ("Meč");
		equipmentOrder.Add ("Věc");
		equipmentOrder.Add ("Štít");
		
		//BaseItemStats emptyItem = new BaseItemStats ();
		emptyItem.ItemID = -1;
		emptyItem.ItemName = "(prázdné)";
		emptyItem.Agility = emptyItem.Armor = emptyItem.Intellect = emptyItem.Stamina = emptyItem.Strength = 0;
		foreach (var item in equipmentOrder) {
			equipmentMap [item] = emptyItem;
		}
	}
	void loadEquipment(){
		initEquipment ();
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM DressedItems WHERE playerID = " + playerID + ";";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			for (int i = 0; i < equipmentOrder.Count; i++) {
				int itemId = reader.GetInt32(i+2);
				equipmentMap[equipmentOrder[ i ] ] = (itemId<0)? emptyItem : ItemsData.itemsData[itemId];
			}
		}
		reader.Close();
		command.Dispose();
		connection.Close();
		SqliteConnection.ClearAllPools();
	}

	void loadActivePotions(){
		string query = "SELECT itemID,ttl FROM ActivePotions WHERE PlayerID = " + playerID + ";";

		activePotions.Clear ();
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		command.CommandText = query;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			activePotions[reader.GetInt32(0)] = reader.GetFloat(1);
		}
		reader.Close();
		command.Dispose();
		connection.Close();
		SqliteConnection.ClearAllPools();
	}

	public void storeInventoryItems(){
		Loot l = GameObject.Find ("Player").GetComponent<Loot> ();
		if (!l) {
			Debug.LogError("Loot obj has not been loaded as player component.");
			return;
		}
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command;
		
		string sqlQuery = "DELETE FROM Inventory WHERE PlayerID = " + playerID + ";";
		command = connection.CreateCommand();
		command.CommandText = sqlQuery;
		command.ExecuteNonQuery();
		command.Dispose();
		
		foreach (var item in l.itemsQuantity.Keys) {
			sqlQuery = "insert into Inventory (playerID,itemID,quantity) values ("+playerID+","+item+","+ l.itemsQuantity[item] +");";
			command = connection.CreateCommand();
			command.CommandText = sqlQuery;
			command.ExecuteNonQuery();
			command.Dispose();        
		}
		
		connection.Close();
		SqliteConnection.ClearAllPools();
	}
	
	void storeEquipment(){
		string query = "UPDATE DressedItems " +
			"SET " +
				" Head = " + equipmentMap[equipmentOrder[0]].ItemID +
				",Chest = " + equipmentMap[equipmentOrder[1]].ItemID +
				",Shoulders = " + equipmentMap[equipmentOrder[2]].ItemID +
				",Hands = " + equipmentMap[equipmentOrder[3]].ItemID +
				",Legs = " + equipmentMap[equipmentOrder[4]].ItemID +
				",Feet = " + equipmentMap[equipmentOrder[5]].ItemID +
				",Sword = " + equipmentMap[equipmentOrder[6]].ItemID +
				",Staff = " + equipmentMap[equipmentOrder[7]].ItemID +
				",Shield = " + equipmentMap[equipmentOrder[8]].ItemID +
			" WHERE PlayerID = " + playerID + ";";
		Debug.Log (query);

		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command;

		command = connection.CreateCommand();
		command.CommandText = query;
		command.ExecuteNonQuery();
		command.Dispose();
		
		connection.Close();
		SqliteConnection.ClearAllPools();
	}

	void storeActivePotions (){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command;
		
		string sqlQuery = "DELETE FROM ActivePotions WHERE PlayerID = " + playerID + ";";
		command = connection.CreateCommand();
		command.CommandText = sqlQuery;
		command.ExecuteNonQuery();
		command.Dispose();
		
		foreach (var item in activePotions.Keys) {
			sqlQuery = "insert into ActivePotions (playerID,itemID,ttl) values ("+playerID+","+item+","+ activePotions[item] +");";
			command = connection.CreateCommand();
			command.CommandText = sqlQuery;
			command.ExecuteNonQuery();
			command.Dispose();        
		}
		
		connection.Close();
		SqliteConnection.ClearAllPools();
	}
	
}
