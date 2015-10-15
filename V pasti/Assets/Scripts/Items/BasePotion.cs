using UnityEngine;
using System.Collections;

public class BasePotion : BaseItemStats
{
	public enum PotionTypes
	{
		HEALTH = 1,
		ENERGY = 2,
		STRENGTH = 3,
		INTELLECT = 4,
		AGILITY = 5,
		STAMINA = 6,
		ARMOR = 7
	}
	private PotionTypes potionType;
	private int potionValue;

	public PotionTypes PotionType
	{
		get{ return potionType;}
		set{ potionType = value;}
	}

	public int PotionValue
	{
		get{ return potionValue;}
		set{ potionValue = value;}
	}
}
