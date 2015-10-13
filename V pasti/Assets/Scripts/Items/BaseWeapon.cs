using UnityEngine;
using System.Collections;

public class BaseWeapon : BaseItemStats 
{
	public enum WeaponTypes
	{
		SWORD = 1,
		STAFF = 2,
		SHIELD = 3
	}
	private WeaponTypes weaponType;
	private int spellEffectID;

	public WeaponTypes WeaponType
	{
		get{ return weaponType;}
		set{ weaponType = value;} 
	}
	
	public int SpellEffectID
	{
		get{ return spellEffectID;}
		set{ spellEffectID = value;} 
	}
}
