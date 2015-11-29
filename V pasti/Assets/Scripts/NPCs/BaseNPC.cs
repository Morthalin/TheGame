using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class BaseNPC : MonoBehaviour
{
    public string creatureName;
    public int attackMin;
    public int attackMax;
    public int health;
    public int healthMax;
    public int energy;
    public int armor;
    public bool hitted;
    private Animator animator;

    void Awake()
    {
        animator = transform.GetComponent<Animator>();
        LoadNPC(creatureName);
    }

    void Update()
    {
        animator.SetInteger("idleState", Random.Range(1, 40));
    }

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
            hitted = false;
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        SqliteConnection.ClearAllPools();
        transform.Find("HPFrame").transform.Find("HPBar").transform.Find("Text").GetComponent<Text>().text = health.ToString() + "/" + healthMax.ToString();
    }
}
