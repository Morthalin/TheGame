using UnityEngine;
using System.Collections;

public class BaseEquipment : BaseItemStats 
{
	public enum EquipTypes
	{
		HEAD = 1,
		CHEST = 2,
		SHOULDERS = 3,
		HANDS = 4,
		LEGS = 5,
		FEET = 6
	}
	private EquipTypes equipType;
	private int specialStatID;

	public EquipTypes EquipType { get; set; }
    public int SpecialStatID { get; set; }
}
