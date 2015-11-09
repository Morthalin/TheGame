using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;

public class BasePlayer: MonoBehaviour
{
    public string playerName;
    public BaseCharacterClass playerClass;
    public Inventory inventory;
    public int strength;
    public int intellect;
    public int agility;
    public int stamina;
    public int health;
    public int energy;
    public int armor;

    public void LoadPlayer (string player)
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
        if (reader.Read())
        {
            //nacitanie premennych
            playerName = reader.GetString(1);
            character = reader.GetString(2);
            inventory = new Inventory(reader.GetInt32(3));
            strength = reader.GetInt32(4);
            intellect = reader.GetInt32(5);
            agility = reader.GetInt32(6);
            stamina = reader.GetInt32(7);
            health = reader.GetInt32(8);
            energy = reader.GetInt32(9);
            armor = reader.GetInt32(10);
        }
        reader.Close();
        command.Dispose();
        connection.Close();

        if (character == "Warrior")
        {
            playerClass = new BaseWarriorClass();
        }
    }
}
