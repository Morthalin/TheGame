using UnityEngine;
using System.Collections;

public class emitOnCollision : MonoBehaviour {

	public GameObject particles;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionStay(Collision other){
        particles.GetComponent<ParticleSystem> ().emissionRate = 200;
	}

	void OnCollisionExit(Collision other){
        particles.GetComponent<ParticleSystem> ().emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
