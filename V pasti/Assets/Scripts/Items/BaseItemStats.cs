using UnityEngine;
using System.Collections;

public class BaseItemStats : BaseItems
{
	//Staty itemu
	private int strength;
	private int intellect;
	private int agility;
	private int stamina;
	private int armor;

	public int Strength
	{
		get{return strength;}
		set{strength = value;}
	}

	public int Intellect
	{
		get{return intellect;}
		set{intellect = value;}
	}
	
	public int Agility
	{
		get{return agility;}
		set{agility = value;}
	}
	
	public int Stamina
	{
		get{return stamina;}
		set{stamina = value;}
	}

	public int Armor
	{
		get{return armor;}
		set{armor = value;}
	}
}
