using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class Loot : MonoBehaviour {
	// corpseList
	public static ArrayList corpseList = new ArrayList();
	public float  lootDistSqrt  = 25;

	// GUI elements
	private Text 	presseText = null;
	private Text    lootList = null;
	private Canvas 	canvas = null;
	private float   lootListLive = 0.0f;

	// soft copy of players inventry object
	private Inventory  inventory; 
	private Dictionary<int, BaseItemStats> itemsData = new Dictionary<int, BaseItemStats>();
	private Dictionary<int, int>           itemsQuantity = new Dictionary<int, int>();
	

	// helper variables
	private bool showPressE = false;
	private bool showInventoryPanel = false;
	private float timesCalseBefore = 0.0f;
	private ArrayList loadedItems = new ArrayList();
	// inventory buttons
	private string description = "";
	private int    inventorySellectedItem = -1;

	// Use this for initialization
	void Start () {
		ItemsData.Load ();
		inventory = new Inventory (1);

		//corpseList.Add (new Corpse("Knight", gameObject.transform.position));

		presseText = GameObject.Find ("Interface").transform.Find ("PressE").GetComponent<Text> ();
		if (presseText == null) {
			Debug.LogError("presseText");
		}
		presseText.enabled = false;
		presseText.text = "<b>[E]</b> prohledat mrtvolu.";

		lootList = GameObject.Find ("Interface").transform.Find ("LootList").GetComponent<Text> ();
		if (lootList == null) {
			Debug.LogError("lootList");
		}
		lootList.enabled = false;
		lootList.text = "Tohle je konstruktorovej text";


		canvas = GameObject.Find ("Interface").GetComponent<Canvas> ();
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
		 
		foreach (Corpse corpse in corpseList) {
			Vector3 v = corpse.pos - pos;
			if( v.sqrMagnitude <= lootDistSqrt )
			{
				if( !showPressE )
				{
					//Debug.Log ("[E] prohledat mrtvolu.");
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
		// ukazat/schovat inventar
		if (Input.GetKeyDown ("i")) {
			if(showInventoryPanel){
				showInventory (false);
				showInventoryPanel = false;
			} else {
				showInventory (true);
				showInventoryPanel = true;
			}
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
			 
			BaseItemStats one = new BaseItemStats();
			one.ItemID = (int)reader.GetInt32(1);
			one.ItemName = (string) reader["itemName"];
			one.ItemDescription = reader.GetString(3);
			one.ItemType = (BaseItemStats.ItemTypes)reader.GetInt32(4); // itemType
			one.Strength = reader.GetInt32(5); // strength
			one.Intellect = reader.GetInt32(6); // intellect
			one.Agility = reader.GetInt32(7); // agility
			one.Stamina = reader.GetInt32(8); // stamina
			one.Armor = reader.GetInt32(9); // armor

			if( UnityEngine.Random.Range(0.0f, 100.0f) <= probability )
			{
				gain += one.ItemName + "\n";
				inventory.itemsID.Add(one.ItemID);
				itemsData[one.ItemID] = one;
				if(  !itemsQuantity.ContainsKey(one.ItemID) )
					itemsQuantity[one.ItemID] = 1;
				else 
					itemsQuantity[one.ItemID] ++;
			}
		}

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
			inventorySellectedItem = -1;
			description = "";
			timesCalseBefore = Time.timeScale;
			Time.timeScale = 0.0f;

		} else {
			Time.timeScale = timesCalseBefore;
		}

	}

	void OnGUI()
	{
		if (showInventoryPanel) {
			int fromX = Screen.width - 320;

			GUI.BeginGroup( new Rect(fromX, 25, 300, 500) );
			GUI.Box(new Rect( 0, 0, 300, 500 ), "Inventář" );
			GUIStyle gs = new GUIStyle(GUI.skin.button);
			gs.fontSize = 10;

			int i = 20, j = 20;
			foreach (var key in itemsQuantity.Keys) {
				if( itemsQuantity[key] > 0 ) {
					BaseItemStats one = itemsData[key];
					String buttonText = one.ItemName + " (" + itemsQuantity[key] + ")";
					if( GUI.Button( new Rect(j, i, 80, 40), buttonText, gs ) ) {
						inventorySellectedItem = key;
						description = one.ItemDescription;
					}
					j += 90;
					if( j >= 350 ){
						j = 20;
						i += 50;
					}
				}
			}

			if( GUI.Button( new Rect(240,400,60, 100), "Použít" ) ){
				if( inventorySellectedItem != -1 ){
					useItem(itemsData[inventorySellectedItem]);
					int rest = --itemsQuantity[inventorySellectedItem];

					if( rest < 1 ){
						description = "";
						inventorySellectedItem = -1;
					}					
				}
			}

			GUI.TextArea( new Rect(0, 400, 240, 100), description );
			GUI.EndGroup();
		}
	}

	void useItem(BaseItemStats item)
	{
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();

		pl.strength += item.Strength; 
		pl.intellect += item.Intellect;
		pl.agility += item.Agility; 
		pl.stamina += item.Stamina; 
		pl.armor += item.Armor;

		if (item.ItemType == BaseItems.ItemTypes.NOTE) {
			//Debug.Log ("NOTE: " + ItemsData.notesData[item.ItemID].NoteText );
			lootList.text = "<b>"+ItemsData.notesData[item.ItemID].NoteText+"</b>";
			lootListLive += 10;
			showLoot(true);
		}
	}
}


