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
    public int energyMax;
    public int armor;
    public int activeArmor;
    public int pause;
    public bool attacking;
    public bool dead = false;
    public int storyCheckpoint;
    private string character;
    public int healthRegen = 5;
    public int energyRegen = 1;

    void Update()
    {
        if(pause != 0)
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
        transform.GetComponent<Animator>().SetBool("death", false);
        LoadScale();

        if (character == "Warrior")
        {
            playerClass = new BaseWarriorClass();
        }

        pause = 0;
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
            healthMax = health = stamina * 100;
            energyMax = energy = reader.GetInt32(7);
            armor = reader.GetInt32(8);
            activeArmor = armor;
            minAttack = reader.GetInt32(9);
            maxAttack = reader.GetInt32(10);
            transform.position = new Vector3(float.Parse(reader[11].ToString()), float.Parse(reader[12].ToString()), float.Parse(reader[13].ToString()));
            transform.rotation = Quaternion.Euler(new Vector3(0, float.Parse(reader[14].ToString()), 0));
            storyCheckpoint = reader.GetInt32(15);
            attacking = false;
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        SqliteConnection.ClearAllPools();
    }

    private void LoadScale()
    {
        transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
    }
}
