using UnityEngine;
using System.Collections;

public class GroulsPatrol : MonoBehaviour {
	public int BecomeEnemyCheckpoint = 30;
	/// <summary>
	/// Skupina ghoulu pred drakem se stane nepratelskou az v pripade kdy 
	/// hrdina odnasi sud zpet, to se stane na storypontu BecomeEnemyCheckpoint
	/// </summary>
	void Start () {
		foreach (var item in transform.GetComponentsInChildren<Transform>()){
			item.gameObject.layer = 27;
		}
	}
	
	// Update is called once per frame
	void Update () {
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (pl && pl.storyCheckpoint == BecomeEnemyCheckpoint) {
			foreach (var item in transform.GetComponentsInChildren<Transform>()){
				item.gameObject.layer = 0;
			}
		}
	}
}
