using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;

public class ItemsData : MonoBehaviour {
	public static Dictionary<int, BaseEquipment> equipmentData = new Dictionary<int, BaseEquipment>();
	public static Dictionary<int, BaseNote> notesData = new Dictionary<int, BaseNote>();
	public static Dictionary<int, BasePotion> potionsData = new Dictionary<int, BasePotion>();
	public static Dictionary<int, BaseWeapon> weaponsData = new Dictionary<int, BaseWeapon>();
	public static Dictionary<int, BaseItemStats> itemsData = new Dictionary<int, BaseItemStats>();

	// Use this for initialization
	public static void Load () {
		loadEquipment ();
		loadNotes ();
		loadPotions ();
		loadWeapons ();
		//loadItems ();
	}

	public static void loadEquipment (){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM Items_Equipment;";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			BaseEquipment be = new BaseEquipment();
			be.ItemID = reader.GetInt32 (1);
			be.EquipType = (BaseEquipment.EquipTypes)reader.GetInt32 (2);
			be.SpecialStatID = reader.GetInt32 (3);
			equipmentData[be.ItemID] = be;
		}
		reader.Close();
		command.Dispose();
		connection.Close();
	}

	public static void loadNotes()
	{
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM Items_Notes;";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			BaseNote one = new BaseNote();
			one.ItemID = reader.GetInt32 (0);
			one.NoteText = reader.GetString (1);
			notesData[one.ItemID] = one;
		}
		reader.Close();
		command.Dispose();
		connection.Close();
	}

	public static void loadPotions (){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM Items_Potions;";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			BasePotion one = new BasePotion();
			one.ItemID = reader.GetInt32 (0);
			one.PotionType = (BasePotion.PotionTypes)reader.GetInt32 (1);
			one.PotionValue = reader.GetInt32 (2);
			one.PotionDuration = reader.GetInt32 (3);
			potionsData[one.ItemID] = one;
		}
		reader.Close();
		command.Dispose();
		connection.Close();
	}
	public static void loadWeapons (){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		string sqlQuery = "SELECT * FROM Items_Weapons;";
		command.CommandText = sqlQuery;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			BaseWeapon one = new BaseWeapon();
			one.ItemID = reader.GetInt32 (0);
			one.WeaponType = (BaseWeapon.WeaponTypes)reader.GetInt32 (1);
			one.SpellEffectID = reader.GetInt32 (2);
			weaponsData[one.ItemID] = one;
		}
		reader.Close();
		command.Dispose();
		connection.Close();
	}

	public static void loadItems (){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		IDbConnection connection;
		
		connection = (IDbConnection)new SqliteConnection(path);
		connection.Open();
		IDbCommand command = connection.CreateCommand();
		
		// Items.* = Items.ID, Items.itemName, Items.itemDescription, Items.itemType, Items.strength, Items.intellect, Items.agility, Items.stamina, Items.armor
		string query = "select Items.* from Items;";
		command.CommandText = query;
		IDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{			
			BaseItemStats one = new BaseItemStats();
			one.ItemID = (int)reader.GetInt32(0);
			one.ItemName = (string) reader["itemName"];
			one.ItemDescription = reader.GetString(2);
			one.ItemType = (BaseItemStats.ItemTypes)reader.GetInt32(3);
			one.Strength = reader.GetInt32(4);
			one.Intellect = reader.GetInt32(5); 
			one.Agility = reader.GetInt32(6);
			one.Stamina = reader.GetInt32(7);
			one.Armor = reader.GetInt32(8);
			itemsData[one.ItemID] = one;
			
		}
		reader.Close ();
		command.Dispose();
		connection.Close();
	}
	
	// Update is called once per frame
	void Update () {
	}
}




