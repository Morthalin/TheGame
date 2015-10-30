using UnityEngine;
using System.Collections;

public class BaseNPC : MonoBehaviour
{
    private string creatureName;
    private int attackMin;
    private int attackMax;
    private int health;
    private int energy;
    private int armor;

    public int CreatureName { get; set; }
    public int AttackMin { get; set; }
    public int AttackMax { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }
    public int Armor { get; set; }
}
