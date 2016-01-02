using UnityEngine;
using System.Collections;

public class StorypointTrigger : MonoBehaviour {
	public int TargetStorypoint = 29;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player" && 
		    other.gameObject.GetComponent<BasePlayer>().storyCheckpoint != TargetStorypoint) {

			other.gameObject.GetComponent<BasePlayer>().storyCheckpoint = TargetStorypoint;
		}
	}
}
