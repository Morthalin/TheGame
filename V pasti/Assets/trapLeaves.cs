using UnityEngine;
using System.Collections;


public class trapLeaves : MonoBehaviour {
	public ParticleSystem particleSys;
	public GameObject leaves;
	// Use this for initialization
	void Start () {
		particleSys.Pause();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			particleSys.Play();
			leaves.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
