using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;

public class BasePlayer
{
    public string playerName;
    private BaseCharacterClass playerClass;
    private Inventory inventory;
    private int strength;
    private int intellect;
    private int agility;
    private int stamina;
    public int health;
    private int energy;
    private int armor;

    public string PlayerName { get; set; }
    public BaseCharacterClass PlayerClass { get; set; }

    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }
    public int Armor { get; set; }

    public BasePlayer(string player)
    {
        //nacitanie dat z DB
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;
        string character = "";

        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM Players WHERE playerName = '" + player + "';";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            //nacitanie premennych
            PlayerName = reader.GetString(1);
            character = reader.GetString(2);
            inventory = new Inventory(reader.GetInt32(3));
            Strength = reader.GetInt32(4);
            Intellect = reader.GetInt32(5);
            Agility = reader.GetInt32(6);
            Stamina = reader.GetInt32(7);
            Health = reader.GetInt32(8);
            Energy = reader.GetInt32(9);
            Armor = reader.GetInt32(10);
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        
        if(character == "Warrior")
        {
            PlayerClass = new BaseWarriorClass();
        }
        Debug.Log(Health.ToString());
    }
}
