using UnityEngine;
using System.Collections;

public class BaseItems
{
	//Charakteristicke promenne itemu
	private string itemName;
	private string itemDescription;
	private int itemID;
	//typ itemu
	public enum ItemTypes
	{
		EQUIPMENT = 1,
		WEAPON = 2,
		POTION = 3,
		NOTE = 4
	}
	private ItemTypes itemType;

	public string ItemName
	{
		get{return itemName;}
		set{itemName = value;}
	}

	public string ItemDescription
	{
		get{return itemDescription;}
		set{itemDescription = value;}
	}
	
	public int ItemID
	{
		get{return itemID;}
		set{itemID = value;}
	}
	
	public ItemTypes ItemType
	{
		get{return itemType;}
		set{itemType = value;}
	}
}
