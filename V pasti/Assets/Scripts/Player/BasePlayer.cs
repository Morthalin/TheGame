using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class BasePlayer: MonoBehaviour
{
    public string playerName;
    public BaseCharacterClass playerClass;
    public Inventory inventory;
    public int minAttack;
    public int maxAttack;
    public int strength;
    public int intellect;
    public int agility;
    public int stamina;
    public int health;
    public int healthMax;
    public int energy;
    public int armor;
    public int activeArmor;
    public bool pause;
    public bool attacking;
    private string character;

    void Update()
    {
        if(pause)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    public void LoadPlayer (string player)
    {
        LoadStats(player);

        if (character == "Warrior")
        {
            playerClass = new BaseWarriorClass();
        }

        pause = false;
    }

    public void LoadStats(string player)
    {
        //nacitanie dat z DB
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;
        character = "";

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
            inventory = new Inventory(reader.GetInt32(0));
            strength = reader.GetInt32(3);
            intellect = reader.GetInt32(4);
            agility = reader.GetInt32(5);
            stamina = reader.GetInt32(6);
            health = stamina * 100;
            healthMax = health;
            energy = reader.GetInt32(7);
            armor = reader.GetInt32(8);
            activeArmor = armor;
            minAttack = reader.GetInt32(9);
            maxAttack = reader.GetInt32(10);
            transform.position = new Vector3(float.Parse(reader[11].ToString()), float.Parse(reader[12].ToString()), float.Parse(reader[13].ToString()));
            attacking = false;
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        SqliteConnection.ClearAllPools();
    }
}
