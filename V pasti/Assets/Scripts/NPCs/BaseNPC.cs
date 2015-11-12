using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;

public class BaseNPC : MonoBehaviour
{
    public string creatureName;
    public int attackMin;
    public int attackMax;
    public int health;
    public int healthMax;
    public int energy;
    public int armor;

    public void LoadNPC(string name)
    {
        //nacitanie dat z DB
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;

        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM Creatures WHERE creatureName = '" + name + "';";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            //nacitanie premennych
            creatureName = reader.GetString(1);
            healthMax = health = reader.GetInt32(2);
            energy = reader.GetInt32(3);
            armor = reader.GetInt32(4);
            attackMin = reader.GetInt32(5);
            attackMax = reader.GetInt32(6);
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        transform.Find("HPFrame").transform.Find("HPBar").transform.Find("Text").GetComponent<Text>().text = health.ToString() + "/" + healthMax.ToString();
    }
}
