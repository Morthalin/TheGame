using UnityEngine;
using System.Collections;

public class BaseCharacterClass
{
	//nazev a popis postavy
	private string characterClassName;
	private string characterClassDescription;
	//staty
	private int strength;
	private int intellect;
	private int agility;
	private int stamina;
	private int armor;

	public string CharacterClassName
	{
		get{ return characterClassName;}
		set{characterClassName = value;}
	}

	public string CharacterClassDescription
	{
		get{ return characterClassDescription;}
		set{characterClassDescription = value;}
	}

	public int Strength
	{
		get{ return strength;}
		set{strength = value;}
	}

	public int Intellect
	{
		get{ return intellect;}
		set{intellect = value;}
	}

	public int Agility
	{
		get{ return agility;}
		set{agility = value;}
	}

	public int Stamina
	{
		get{ return stamina;}
		set{stamina = value;}
	}

	public int Armor
	{
		get{ return armor;}
		set{armor = value;}
	}
}
