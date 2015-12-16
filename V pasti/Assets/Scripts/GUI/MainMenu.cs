using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class MainMenu : MonoBehaviour
{

	public Transform mainMenu;
	public Transform newMenu = null;
	public Transform settingsMenu;
	public Transform exitMenu;

    void Start ()
    {
        if(!mainMenu)
        {
            Debug.LogError("Missing main menu!");
        }

        if (!newMenu && Application.loadedLevelName != "scene")
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
            transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
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
            SqliteConnection.ClearAllPools();
            transform.FindChild("Hraci").gameObject.SetActive(true);
        }
    }

	public void settingsPressed()
    {
        if (transform.FindChild("Hraci").gameObject.activeSelf)
        {
            transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
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
            transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
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
            GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().jmeno = transform.FindChild("Hraci").GetComponent<Dropdown>().options[transform.FindChild("Hraci").GetComponent<Dropdown>().value].text;
        }
        else
        {
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
    }

    public void playerSelectedIngame()
    {
        if (transform.FindChild("Hraci").GetComponent<Dropdown>().value != -1)
        {
            transform.FindChild("loadHruBut").gameObject.SetActive(true);
        }
        else
        {
            transform.FindChild("loadHruBut").gameObject.SetActive(false);
        }
    }

    public void loadGamePressed()
    {
        transform.parent.GetComponent<LoadingSceen>().loading++;
        Application.LoadLevel("scene");
    }

    public void loadGamePressedIngame()
    {
        GameObject.Find("Interface").GetComponent<LoadPlayer>().loadPlayer(transform.FindChild("Hraci").GetComponent<Dropdown>().options[transform.FindChild("Hraci").GetComponent<Dropdown>().value].text);
        //if (transform.FindChild("Hraci").gameObject.activeSelf)
        //{
        //    transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
        //    transform.FindChild("Hraci").gameObject.SetActive(false);
        //    transform.FindChild("loadHruBut").gameObject.SetActive(false);
        //}
        //mainMenu.parent.gameObject.SetActive(false);
        //mainMenu.gameObject.SetActive(false);
        //exitMenu.gameObject.SetActive(false);
        //settingsMenu.gameObject.SetActive(false);
        transform.parent.GetComponent<LoadingSceen>().loading++;
        if (GameObject.Find("LoadPlayer"))
        {
            GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().jmeno = transform.FindChild("Hraci").GetComponent<Dropdown>().options[transform.FindChild("Hraci").GetComponent<Dropdown>().value].text;
        }
        Application.LoadLevel("scene");
    }

    public void savePressed()
    {
        LoadingSceen loading = GameObject.Find("Menu").GetComponent<LoadingSceen>();
        BasePlayer pl = GameObject.Find("Player").GetComponent<BasePlayer>();
        
        if (pl.health > 0)
        {
            loading.loading++;
            pl.inventory.Save();
            string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
            IDbConnection connection;

            connection = (IDbConnection)new SqliteConnection(path);
            connection.Open();
            IDbCommand command = connection.CreateCommand();
            string sqlQuery = "UPDATE Players SET positionX = " + GameObject.Find("Player").transform.position.x.ToString() + @",
                                positionY = " + GameObject.Find("Player").transform.position.y.ToString() + @" + 1,
                                positionZ = " + GameObject.Find("Player").transform.position.z.ToString() + @",
                                rotationY = " + GameObject.Find("Player").transform.rotation.eulerAngles.y + @",
                                storyLine = " + GameObject.Find("Player").GetComponent<BasePlayer>().storyCheckpoint.ToString() + @" 
                               WHERE playerName = '" + GameObject.Find("Player").GetComponent<BasePlayer>().playerName + "';";
            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            SqliteConnection.ClearAllPools();
            pl.pause--;
            loading.loading--;
            if (transform.FindChild("Hraci").gameObject.activeSelf)
            {
                transform.FindChild("Hraci").GetComponent<Dropdown>().value = -1;
                transform.FindChild("Hraci").gameObject.SetActive(false);
                transform.FindChild("loadHruBut").gameObject.SetActive(false);
            }
            mainMenu.parent.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(false);
            exitMenu.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
        }
        
    }
}
