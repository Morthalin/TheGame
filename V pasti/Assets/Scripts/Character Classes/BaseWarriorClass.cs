using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class BaseWarriorClass : BaseCharacterClass
{
	public BaseWarriorClass()
	{
        //nacitanie dat z DB
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;
        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM ClassInfo WHERE className = 'Warrior';";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            //nacitanie premennych
            CharacterClassName = reader.GetString(1);
            CharacterClassDescription = reader.GetString(2);
            Strength = reader.GetInt32(3);
            Intellect = reader.GetInt32(4);
            Agility = reader.GetInt32(5);
            Stamina = reader.GetInt32(6);
            Health = reader.GetInt32(7);
            Energy = reader.GetInt32(8);
            Armor = reader.GetInt32(9);
        }
        reader.Close();
        command.Dispose();
        connection.Close();
    }
}
