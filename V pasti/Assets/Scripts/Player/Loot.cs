using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class Loot : MonoBehaviour {
	//PlayerPartsRenderers
	/*
	index - Head 0
			Chest 1-3
			Shoulders 4-5
			Arms and hands 6-11
			Legs 12-15
			Foots 16-17
	 */
	public Renderer [] PlayerPartsRenderers;

	/*newMaterials index - head 0, other 1*/
	public Material [] newMaterials;

	// corpseList
	public static ArrayList corpseList = new ArrayList();
	public float  lootDistSqrt  = 25;

	// GUI elements
	private Text 	presseText = null;
	private Text    lootList = null;
	private Canvas 	canvas = null;
	private float   lootListLive = 0.0f;

	// soft copy of players inventry object
	private Dictionary<int, BaseItemStats> itemsData;
	public  Dictionary<int, int>           itemsQuantity;

	// equipment
	private Dictionary<string, BaseItemStats>   equipmentMap = null;
	private List<string>						equipmentOrder = null;
	// active potions
	private Dictionary<int, float> 				activePotions = null;
	

	// helper variables
	private bool showPressE = false;
	private bool showInventoryPanel = false;
	//private float timesCalseBefore = 0.0f;
	//private ArrayList loadedItems = new ArrayList();
	// inventory buttons
	private string description = "";
	private int    inventorySellectedItem = -1;

	// Use this for initialization
	void Start () {

		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		itemsQuantity = pl.inventory.itemsQuantity;
		itemsData = ItemsData.itemsData;
		initEquipment ();
		

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
        if((GameObject.Find("Player").GetComponent<BasePlayer>().pause == 0 && showInventoryPanel))
        {
            showInventory(false);
            showInventoryPanel = false;
        }

		if (Input.GetKeyDown("c") || Input.GetKeyDown ("i") || (GameObject.Find("Player").GetComponent<BasePlayer>().pause == 0 && showInventoryPanel)) {
			if(showInventoryPanel){
				showInventory (false);
				showInventoryPanel = false;
                transform.GetComponent<BasePlayer>().pause--;
            } else {
				showInventory (true);
				showInventoryPanel = true;
                transform.GetComponent<BasePlayer>().pause++;
            }
		}
		List<int> keys = new List<int> (activePotions.Keys);
		foreach (var item in keys) {
			if(activePotions[item] > 0.0f )
			{
				activePotions[item] -= Time.deltaTime;
				if(activePotions[item] < 0.0f){
					potionAction(item, false);
					activePotions.Remove(item);
				}
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
				//inventory.itemsID.Add(one.ItemID);
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
		}

	}

	void OnGUI()
	{
		displayActivePotions ();
		if (GameObject.Find ("Menu"))
			return;
		if (showInventoryPanel) {
			int fromX = Screen.width - 320;
			GUI.Box(new Rect( fromX-200, 25, 300+200, 450 ), "Inventář" );
			GUI.BeginGroup( new Rect(fromX, 25, 300, 500) );
			//GUI.Box(new Rect( 0, 0, 300, 500 ), "Inventář" );
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
					if( j >= 250 ){
						j = 20;
						i += 50;
					}
				}
			}
			GUI.EndGroup();
			GUI.TextArea( new Rect(fromX-188, 350, 350, 100), description );
			if( GUI.Button( new Rect(fromX-190+350,350,125, 100), "Použít" ) ){
				if( inventorySellectedItem != -1 ){
					useItem(itemsData[inventorySellectedItem]);
					int rest = --itemsQuantity[inventorySellectedItem];
					
					if( rest < 1 ){
						description = "";
						inventorySellectedItem = -1;
					}					
				}
			}

			displayEquipment( new Rect(fromX - 202, 25, 192, 340));
		}
	}
	private BaseItemStats emptyItem = new BaseItemStats ();
	void initEquipment(){
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		emptyItem = pl.inventory.emptyItem;
		equipmentMap = pl.inventory.equipmentMap;
		equipmentOrder = pl.inventory.equipmentOrder;
		activePotions = pl.inventory.activePotions;
	}

	void displayEquipment(Rect grouprec){
		GUI.BeginGroup (grouprec);
		//GUI.Box (new Rect(0, 0, grouprec.width, grouprec.height), "Oblečení a zbraně");
		GUIStyle gs = new GUIStyle(GUI.skin.textField);
		gs.alignment = TextAnchor.MiddleCenter;
		Rect rc = new Rect (15, 30, 60, 30);
		Rect rc2 = new Rect (rc.x+rc.width+2, 30, 100, 30);
		foreach (var item in equipmentOrder) {
			GUI.TextField(rc, item );
			GUI.TextField(rc2,equipmentMap[item].ItemName,gs);
			rc.y += 32;
			rc2.y += 32;
		}
		GUI.EndGroup ();
	}

	void displayActivePotions (){
		Rect rc = new Rect (0, 0, 100, 30);
		foreach (var item in activePotions.Keys) {
			GUI.TextField(rc, itemsData[item].ItemName+" "+ (int)activePotions[item] +" s" );
			rc.x += 100; 
		}
	}

	void changeMaterial(int from, int to, int matIndex){
		for(int i = from; i <= to; i++){
			PlayerPartsRenderers[i].material = newMaterials[matIndex];
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
			lootList.text = "<b>" + ItemsData.notesData [item.ItemID].NoteText + "</b>";
			lootListLive += 10;
			showLoot (true);
		} else if (item.ItemType == BaseItems.ItemTypes.EQUIPMENT) {

			BaseEquipment.EquipTypes etype = ItemsData.equipmentData [item.ItemID].EquipType;
			int index = (int)etype - 1;
			// kdyz neni prazdne, odecteme vyhody a pridame do inventare
			if (equipmentMap [equipmentOrder [index]].ItemName != emptyItem.ItemName) {
				BaseItemStats bis = equipmentMap [equipmentOrder [index]];
				pl.strength -= bis.Strength; 
				pl.intellect -= bis.Intellect;
				pl.agility -= bis.Agility; 
				pl.stamina -= bis.Stamina; 
				pl.armor -= bis.Armor;
				// pridani do inventare
				itemsQuantity [bis.ItemID]++;
			}
			equipmentMap [equipmentOrder [index]] = item;
			switch((int)etype){
			case 1 :{changeMaterial(0,0,0); break;}
			case 2 :{changeMaterial(1,3,1); break;}
			case 3 :{changeMaterial(4,5,1); break;}
			case 4 :{changeMaterial(6,11,1); break;}
			case 5 :{changeMaterial(12,15,1); break;}
			case 6 :{changeMaterial(16,17,1); break;}
			}
			//zmena materialu

		} else if (item.ItemType == BaseItems.ItemTypes.WEAPON) {
			BaseWeapon.WeaponTypes wtype = ItemsData.weaponsData [item.ItemID].WeaponType;
			int index = (int)wtype - 1 + 6;
			// kdyz neni prazdne, odecteme vyhody a pridame do inventare
			if (equipmentMap [equipmentOrder [index]].ItemName != emptyItem.ItemName) {
				BaseItemStats bis = equipmentMap [equipmentOrder [index]];
				pl.strength -= bis.Strength; 
				pl.intellect -= bis.Intellect;
				pl.agility -= bis.Agility; 
				pl.stamina -= bis.Stamina; 
				pl.armor -= bis.Armor;
				// pridani do inventare
				itemsQuantity [bis.ItemID]++;
			}
			equipmentMap [equipmentOrder [index]] = item;
		} else if (item.ItemType == BaseItems.ItemTypes.POTION) {
			pl.strength -= item.Strength; 
			pl.intellect -= item.Intellect;
			pl.agility -= item.Agility; 
			pl.stamina -= item.Stamina; 
			pl.armor -= item.Armor;
			BasePotion bp = ItemsData.potionsData[item.ItemID];
			if(activePotions.ContainsKey(item.ItemID)){
				activePotions[item.ItemID] += bp.PotionDuration;
			} else {
				activePotions[item.ItemID] = bp.PotionDuration;
				potionAction(item.ItemID, true);
			}
		}

	}

	// if apply is true then add value else sub value 
	private void potionAction(int itid, bool apply)
	{
		BasePotion bp = ItemsData.potionsData[itid];
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();

		if (! apply)
			bp.PotionValue *= -1;

		switch (bp.PotionType) {
		case BasePotion.PotionTypes.AGILITY 	: pl.agility += bp.PotionValue; break;
		case BasePotion.PotionTypes.ARMOR 		: pl.armor += bp.PotionValue; break;
		case BasePotion.PotionTypes.ENERGY 		: pl.energyMax += bp.PotionValue; pl.energy = Math.Max(pl.energy,pl.energy + bp.PotionValue); break;
		case BasePotion.PotionTypes.HEALTH 		: pl.healthMax += bp.PotionValue; pl.health = Math.Max(pl.health,pl.health + bp.PotionValue); break;
		case BasePotion.PotionTypes.INTELLECT 	: pl.intellect += bp.PotionValue; break;
		case BasePotion.PotionTypes.STAMINA  	: pl.stamina += bp.PotionValue; break;
		case BasePotion.PotionTypes.STRENGTH 	: pl.strength += bp.PotionValue; break;
		default:
			break;
		}

		if (! apply)
			bp.PotionValue *= -1;
	}
}


