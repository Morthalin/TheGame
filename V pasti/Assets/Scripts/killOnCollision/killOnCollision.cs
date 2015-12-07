using UnityEngine;
using System.Collections;

public class killOnCollision : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "Player"){
			other.gameObject.GetComponent<BasePlayer> ().health = 0;	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
