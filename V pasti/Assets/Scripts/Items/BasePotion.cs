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
    private int potionDuration;

	public PotionTypes PotionType { get; set; }
	public int PotionValue { get; set; }
    public int PotionDuration { get; set; }
}
