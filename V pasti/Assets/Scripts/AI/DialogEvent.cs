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
	private bool isself = false;

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
        if(start /*|| Input.GetKeyDown(KeyCode.T)/**/)
        {
            active = true;
            start = true;
			isself = true;
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
            if (dialogBox.gameObject.activeSelf && isself )
            {
                dialogBox.gameObject.SetActive(false);
                Time.timeScale = 1f;
                GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
				isself = false;
				applyPostTalkEffect();
            }
        }
	}

	void applyPostTalkEffect(){
		BasePlayer player = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!player) {
			Debug.LogError("Events.StoryPointIncrem:There is no player object.");
			return;
		}
		// po monologu
		if (player.storyCheckpoint == 0 && dialog == 1)
			player.storyCheckpoint = 1;
		// vypije vyprostovak
		if (player.storyCheckpoint == 1 && dialog == 2)
			player.storyCheckpoint = 2;
		// proroctvi + vysvobod nas + byt hrdinou mi slusi
		else if (player.storyCheckpoint == 3 && dialog == 3)
			player.storyCheckpoint = 4;
		// bylinkarka zadala ukol.
		else if (player.storyCheckpoint == 4 && dialog == 5)
			player.storyCheckpoint = 5;
		else if (player.storyCheckpoint == 6 && dialog == 6)
			player.storyCheckpoint = 7;
		else if(player.storyCheckpoint == 7 && dialog == 7)
		{
			player.healthMax += 150;
			player.agility   += 10;
			player.storyCheckpoint ++;
		}
			
		
	}
}
