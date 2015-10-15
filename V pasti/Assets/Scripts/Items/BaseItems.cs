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
		NOTE = 4,
		QUEST = 5
	}
	private ItemTypes itemType;

	public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemID { get; set; }
    public ItemTypes ItemType { get; set; }
}
