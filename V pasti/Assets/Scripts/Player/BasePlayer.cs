using UnityEngine;
using System.Collections;

public class BasePlayer
{
    private string playerName;
    private int playerLevel;
    private BaseCharacterClass playerClass;
    private int strength;
    private int intellect;
    private int agility;
    private int stamina;
    private int health;
    private int energy;
    private int armor;

    public int PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public int PlayerClass { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }
    public int Armor { get; set; }
}
