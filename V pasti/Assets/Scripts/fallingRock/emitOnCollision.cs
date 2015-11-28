using UnityEngine;
using System.Collections;

public class emitOnCollision : MonoBehaviour {

	public GameObject particleSystem;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionStay(Collision other){
		particleSystem.GetComponent<ParticleSystem> ().emissionRate = 200;
	}

	void OnCollisionExit(Collision other){
		particleSystem.GetComponent<ParticleSystem> ().emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
