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

	public string CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public int Armor { get; set; }
}
