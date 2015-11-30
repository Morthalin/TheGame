using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System.Text;
using System;

public class DialogEvent : MonoBehaviour
{
    private bool active = false;
    public bool start = false;
    private Transform dialogBox;
    public int dialog = 0;
    private int order = 0;

	void Awake ()
    {
        dialogBox = GameObject.Find("Interface").transform.FindChild("DialogBox");
        if(!dialogBox)
        {
            Debug.LogError("Missing DialogBox prefab!");
        }

        if(dialog == 0)
        {
            Debug.LogError("Missing DialogID!");
        }
	}
	
	void Update ()
    {
        if(start || Input.GetKeyDown(KeyCode.T))
        {
            active = true;
            start = true;
            GameObject.Find("Player").GetComponent<BasePlayer>().pause++;
            Time.timeScale = 0f;
        }

	    if(active)
        {
            if (Input.GetMouseButtonUp(0) || start)
            {
                start = false;
                order++;
                string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
                IDbConnection connection;

                connection = (IDbConnection)new SqliteConnection(path);
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                string sqlQuery = "SELECT text, portraitAdress FROM Dialogs WHERE dialog = '" + dialog.ToString() + "' AND poradi = " + order.ToString() + ";";
                command.CommandText = sqlQuery;
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dialogBox.FindChild("SpeakerImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Portraits/" + reader[1].ToString());
                    dialogBox.FindChild("Text").GetComponent<Text>().text = reader[0].ToString();
                    if(!dialogBox.gameObject.activeSelf)
                    {
                        dialogBox.gameObject.SetActive(true);
                    }
                }
                else
                {
                    active = false;
                }

                reader.Close();
                command.Dispose();
                connection.Close();
            }
        }
        else
        {
            if (dialogBox.gameObject.activeSelf)
            {
                dialogBox.gameObject.SetActive(false);
                Time.timeScale = 1f;
                GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
            }
        }
	}
}
