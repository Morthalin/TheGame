using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class Loot : MonoBehaviour {
	// corpseList
	public static ArrayList corpseList = new ArrayList();

	// GUI elements
	private Text 	presseText = null;
	private Text    lootList = null;
	private Canvas 	canvas = null;
	private float   lootListLive = 0.0f;

	// soft copy of players inventry object
	private Inventory  inventory; 

	// helper variables
	private bool showPressE = false;
	private bool showInventoryPanel = false;
	private float timesCalseBefore = 0.0f;
	private ArrayList loadedItems = new ArrayList();

	// Use this for initialization
	void Start () {
		inventory = new Inventory (1);
		inventory.itemsID.Add (2);
		Vector3 pos = gameObject.transform.position;
		Debug.Log ("Ahoj nastartovala se Loot.cs " + pos.ToString());
		Vector3 v = new Vector3 (3, 3, 3);
		Debug.Log ("Distance is " + v.sqrMagnitude );

		presseText = GameObject.Find ("Canvas").transform.Find ("PressE").GetComponent<Text> ();
		if (presseText == null) {
			Debug.LogError("presseText");
		}
		presseText.enabled = false;
		presseText.text = "<b>[E]</b> prohledat mrtvolu.";

		lootList = GameObject.Find ("Canvas").transform.Find ("LootList").GetComponent<Text> ();
		if (lootList == null) {
			Debug.LogError("lootList");
		}
		lootList.enabled = false;
		lootList.text = "Tohle je konstruktorovej text";


		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		if (canvas == null) {
			Debug.LogError("canvas");
		}
		canvas.enabled = true;

		presseText.GetComponentInChildren<RectTransform>().gameObject.SetActive(false);
		lootList.GetComponentInChildren<RectTransform>().gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	public void Update () {
		Vector3 pos = gameObject.transform.position;
		float lootDistSqrt = 4;
		 
		foreach (Corpse corpse in corpseList) {
			Vector3 v = corpse.pos - pos;
			if( v.sqrMagnitude <= lootDistSqrt )
			{
				if( !showPressE )
				{
					Debug.Log ("[E] prohledat mrtvolu.");
					showPressE = true;
					showPresse(true);
				}
			}
			else {
				if(showPressE)
				{
					showPresse(false);
				}
				showPressE = false;

			}
			if (showPressE && Input.GetKeyDown("e") ) {
				getItems(corpse.name);
				corpseList.Remove( corpse );
				showPresse(false);
				showPressE = false;
				break;
			}
		}

		if (lootListLive > 0.0f) {
			lootListLive -= Time.deltaTime;
			if( lootListLive <= 1e-6 )
			{
				showLoot(false);
			}
		}

		if (showInventoryPanel == false && Input.GetKeyDown ("i")) {
			showInventory (true);
			showInventoryPanel = true;
		}
		if (showInventoryPanel && Input.GetKeyDown (KeyCode.Escape)) {
			showInventory (false);
			showInventoryPanel = false;
		}

	}

	private void getItems( string name ){
		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		SqliteConnection connection;
		
		connection = new SqliteConnection(path);
		connection.Open();

		// Items.* = Items.ID, Items.itemName, Items.itemDescription, Items.itemType, Items.strength, Items.intellect, Items.agility, Items.stamina, Items.armor
		string query = "select Loot.lootChance, Items.* from Creatures join Loot on Creatures.ID = creatureID "
					 + "join Items on ItemID = items.ID "
					 + "where creatureName = '" + name + "';";
		Debug.Log (query);
		SqliteCommand command = new SqliteCommand (query, connection);
		SqliteDataReader reader = command.ExecuteReader ();

		string gain = "<b>";
		while (reader.Read())
		{
			float probability = (float)reader.GetFloat(0);
			int id = (int)reader.GetInt32(1);
			string itsname = (string) reader["itemName"];
		/*	reader.GetString(3); // itemDescription
			reader.GetInt32(4); // itemType
			reader.GetInt32(5); // strength
			reader.GetInt32(6); // intellect
			reader.GetInt32(7); // agility
			reader.GetInt32(8); // stamina
			reader.GetInt32(9); // armor
		    */

			if( UnityEngine.Random.Range(0.0f, 100.0f) <= probability )
			{
				gain += itsname + "\n";
				inventory.itemsID.Add(id);
			}
		}
		Debug.Log (gain);
		lootList.text = gain + "</b>";
		showLoot (true);

		lootListLive = 2.0f;
		reader.Close ();
		command.Dispose();
		connection.Close();

	}

	void showPresse( bool show )
	{
		presseText.enabled = show;
		presseText.gameObject.SetActive(show);
	}

	void showLoot( bool show )
	{
		lootList.enabled = show;
		lootList.gameObject.SetActive (show);
	}

	// INVENTORY
	void showInventory( bool show )
	{
		if (show) {
			timesCalseBefore = Time.timeScale;
			Time.timeScale = 0.0f;

			foreach (int itemid in inventory.itemsID) {
				showInventoryItem(itemid);
			}
		} else {
			Time.timeScale = timesCalseBefore;
		}

	}

	void showInventoryItem (int id)
	{
		string query = "select itemName,itemDescription,itemType,strength,intellect,agility,stamina,armor from items where Items.ID == " + id;

		string path = "URI=file:" + Application.dataPath + "/Database/Database.s3db";
		SqliteConnection connection;
		connection = new SqliteConnection(path);
		connection.Open();

		Debug.Log (query);
		SqliteCommand command = new SqliteCommand (query, connection);
		SqliteDataReader reader = command.ExecuteReader ();
		
		string gain = "<b>";
		if (reader.Read())
		{
			string itsname = (string) reader["itemName"];
			string itsdesc = (string) reader["itemDescription"];
			int    itstype = reader.GetInt32(2); //itemType
			/*	
			reader.GetInt32(3); // strength
			reader.GetInt32(4); // intellect
			reader.GetInt32(5); // agility
			reader.GetInt32(6); // stamina
			reader.GetInt32(7); // armor
		    */
			gain += itsname +" "+itsdesc +" "+ itstype;
			loadedItems.Add(itsdesc);

		}
		Debug.Log (gain);
				
		reader.Close ();
		command.Dispose();
		connection.Close();
	}
	private string desctription = "zatim nic";
	void OnGUI()
	{
		if (showInventoryPanel) {
			int fromX = Screen.width - 320;
			int toX  = fromX +300;
			int cnt = 0;

			GUI.BeginGroup( new Rect(fromX, 25, 300, 500) );
			GUI.Box(new Rect( 0, 0, 300, 500 ), "Inventář" );
			for (int i = 20; i < 350; i+=50) {
				for (int j = 20; j < 260; j+=45) {
					if( ++cnt > inventory.itemsID.Count)
						goto konec;
					if( GUI.Button( new Rect(j, i, 40, 40), "neco" ) )
						desctription = "popis " + i + " " + j;
				}
			}
		konec:
			GUI.TextArea( new Rect(0, 400, 300, 100), desctription );
			if( GUI.Button( new Rect(220,440,60, 40), "Použít" ) ){
			}
			GUI.EndGroup();
		}
	}
}


