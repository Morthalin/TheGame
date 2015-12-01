using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class NewGameMenu : MonoBehaviour
{
	public Transform mainMenu;
    private Transform errorText;
    private Transform info;

	void Awake ()
    {
		if(!mainMenu)
        {
            Debug.LogError("Missing main menu reference!");
        }

        errorText = transform.FindChild("ErrorText");
        if(!errorText)
        {
            Debug.LogError("Missing errorText prefab!");
        }
        else
        {
            errorText.gameObject.SetActive(false);
        }

        info = transform.FindChild("InfoTable");
        if(!info)
        {
            Debug.LogError("Missing info box!");
        }
        else
        {
            LoadInfo();
        }
    }
	
	public void PlayPressed()
	{
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;

        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM Players WHERE playerName = '" + transform.FindChild("PlayerName").FindChild("Text").GetComponent<Text>().text + "';";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            errorText.gameObject.SetActive(true);
            errorText.FindChild("Text").GetComponent<Text>().text = "Jméno hráče již existuje!";
            reader.Close();
            command.Dispose();
            connection.Close();
            return;
        }
        else
        {
            if(errorText.gameObject.activeSelf)
            {
                errorText.gameObject.SetActive(false);
            }
        }
        reader.Close();
        command.Dispose();

        string attack = info.FindChild("Attack").FindChild("Value").GetComponent<Text>().text;

        command = connection.CreateCommand();
        sqlQuery = @"INSERT INTO Players (playerName, className, strength, intellect, agility, stamina, energy, armor, minAttack, maxAttack, positionX, positionY, positionZ) VALUES 
                    ('" + transform.FindChild("PlayerName").FindChild("Text").GetComponent<Text>().text + @"', 
                        'Warrior',
                        '" + info.FindChild("Strength").FindChild("Value").GetComponent<Text>().text  + @"',
                        '" + info.FindChild("Intellect").FindChild("Value").GetComponent<Text>().text  + @"',
                        '" + info.FindChild("Agility").FindChild("Value").GetComponent<Text>().text + @"',
                        '" + info.FindChild("Stamina").FindChild("Value").GetComponent<Text>().text + @"',
                        '" + info.FindChild("Energy").FindChild("Value").GetComponent<Text>().text + @"',
                        '" + info.FindChild("Armor").FindChild("Value").GetComponent<Text>().text + @"',
                        '" + attack.Substring(0, attack.IndexOf('-') - 1) + @"',
                        '" + attack.Substring(attack.IndexOf('-') + 2) + @"',
                        '404', '66.5', '814');";
        command.CommandText = sqlQuery;
        command.ExecuteNonQuery();
        command.Dispose();
        connection.Close();
        SqliteConnection.ClearAllPools();
        GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().jmeno = transform.FindChild("PlayerName").FindChild("Text").GetComponent<Text>().text;
        transform.parent.GetComponent<LoadingSceen>().loading++;
        Application.LoadLevel("scene");
    }

    void LoadInfo()
    {
        string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
        IDbConnection connection;

        connection = (IDbConnection)new SqliteConnection(path);
        connection.Open();
        IDbCommand command = connection.CreateCommand();
        string sqlQuery = "SELECT * FROM ClassInfo WHERE ClassName = 'Warrior';";
        command.CommandText = sqlQuery;
        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            info.FindChild("ClassName").FindChild("Value").GetComponent<Text>().text = "Rytíř";
            info.FindChild("Strength").FindChild("Value").GetComponent<Text>().text = reader["strength"].ToString();
            info.FindChild("Intellect").FindChild("Value").GetComponent<Text>().text = reader["intellect"].ToString();
            info.FindChild("Agility").FindChild("Value").GetComponent<Text>().text = reader["agility"].ToString();
            info.FindChild("Stamina").FindChild("Value").GetComponent<Text>().text = reader["stamina"].ToString();
            info.FindChild("Energy").FindChild("Value").GetComponent<Text>().text = reader["energy"].ToString();
            info.FindChild("Armor").FindChild("Value").GetComponent<Text>().text = reader["armor"].ToString();
            info.FindChild("Attack").FindChild("Value").GetComponent<Text>().text = reader["minAttack"].ToString() + " - " + reader["maxAttack"].ToString();
        }
        else
        {
            if (errorText.gameObject.activeSelf)
            {
                errorText.gameObject.SetActive(false);
            }
        }
        reader.Close();
        command.Dispose();
        connection.Close();
        SqliteConnection.ClearAllPools();
    }
    
    public void NavratPressed()
    {
        mainMenu.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
