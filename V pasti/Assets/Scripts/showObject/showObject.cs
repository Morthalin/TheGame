using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SphereCollider))]


public class showObject : MonoBehaviour {

	// Use this for initialization
	public GameObject NPCs;
	public GameObject items;
	SphereCollider sc;
	void Start () {
		sc = GetComponent<SphereCollider>();
		sc.isTrigger = true;
	}

	void OnTriggerExit (Collider other) {
		if (NPCs) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Exit");
				NPCs.SetActive (false);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Exit");
				NPCs.SetActive (false);
			}
		}
	}

	void OnTriggerStay (Collider other) {
		if (NPCs) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (NPCs) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
