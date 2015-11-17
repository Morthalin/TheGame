using UnityEngine;
using System.Collections;

public class CorpseGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Corpse next = new Corpse ("kosticka", gameObject.transform.position); /* jmeno v DB, pozice umrtí.*/
		Loot.corpseList.Add (next);
		Loot.corpseList.Add (next);
		Loot.corpseList.Add (next);
		Loot.corpseList.Add (next);
		Loot.corpseList.Add (next);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

