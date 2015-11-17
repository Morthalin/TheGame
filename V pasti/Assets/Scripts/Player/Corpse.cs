using UnityEngine;
using System.Collections;

public class Corpse
{
	public Corpse(string nameInDB, Vector3 positionOfDie)
	{
		name = nameInDB;
		pos = positionOfDie;
		//visited = false;
	}
	
	public string 		name;
	public Vector3 		pos;
	//public bool 		visited;
}
