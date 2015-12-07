using UnityEngine;
using System.Collections;

public class killByRock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	private bool hitted = false;

	void OnTriggerStay(Collider other){
		OnColliderEnter (other);
	}

	void OnColliderEnter(Collider other) {
		if (other.gameObject.name == "Player" && !hitted) {
			other.gameObject.GetComponent<BasePlayer>().health = 0;
			other.gameObject.GetComponent<Transform>().localScale -= 
											new Vector3(0.0f, 0.8f*(other.gameObject.GetComponent<Transform>().localScale.y),0.0f);
			Debug.Log("Player hit!");
			hitted = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
