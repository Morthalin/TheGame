using UnityEngine;
using System.Collections;

public class BaseWarriorClass : BaseCharacterClass
{
	public BaseWarriorClass()
	{
		//naplneni data postavy
		CharacterClassName = "Warrior";
		CharacterClassDescription = "";
		Strength = 15;
		Intellect = 10;
		Agility = 11;
		Stamina = 13;
	}
}
