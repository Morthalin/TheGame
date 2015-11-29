using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;

public class MainMenu : MonoBehaviour
{

	public Transform mainMenu;
	public Transform newMenu;
	public Transform settingsMenu;
	public Transform exitMenu;

    void Start ()
    {
        if(!mainMenu)
        {
            Debug.LogError("Missing main menu!");
        }

        if (!newMenu)
        {
            Debug.LogError("Missing new character menu!");
        }

        if (!settingsMenu)
        {
            Debug.LogError("Missing settings menu!");
        }

        if (!exitMenu)
        {
            Debug.LogError("Missing exit menu!");
        }

		mainMenu.gameObject.SetActive(true);
		newMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        exitMenu.gameObject.SetActive(false);
    }
	
	public void newGamePressed()
	{
        if (transform.FindChild("Hraci").gameObject.activeSelf)
        {
            transform.FindChild("Hraci").gameObject.SetActive(false);
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
        mainMenu.gameObject.SetActive(false);
        newMenu.gameObject.SetActive(true);
    }

	public void loadPlayerPressed()
    {
        if(transform.FindChild("Hraci").gameObject.activeSelf)
        {
            transform.FindChild("Hraci").gameObject.SetActive(false);
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("Hraci").GetComponent<Dropdown>().options.Clear();
            transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
            string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
            IDbConnection connection;

            connection = (IDbConnection)new SqliteConnection(path);
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            string sqlQuery = "SELECT playerName FROM Players;";
            command.CommandText = sqlQuery;
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                transform.FindChild("Hraci").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(reader[0].ToString()));
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            transform.FindChild("Hraci").gameObject.SetActive(true);
        }
    }

	public void settingsPressed()
    {
        if (transform.FindChild("Hraci").gameObject.activeSelf)
        {
            transform.FindChild("Hraci").gameObject.SetActive(false);
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

	public void exitGamePressed()
    {
        if (transform.FindChild("Hraci").gameObject.activeSelf)
        {
            transform.FindChild("Hraci").gameObject.SetActive(false);
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
        mainMenu.gameObject.SetActive(false);
        exitMenu.gameObject.SetActive(true);
    }

    public void playerSelected()
    {
        if (transform.FindChild("Hraci").GetComponent<Dropdown>().value != -1)
        {
            transform.FindChild("loadHruBut").gameObject.SetActive(true);
            GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().name = transform.FindChild("Hraci").GetComponent<Dropdown>().options[transform.FindChild("Hraci").GetComponent<Dropdown>().value].text;
        }
        else
        {
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
    }

    public void loadGamePressed()
    {
        Application.LoadLevel("scene");
    }
}
