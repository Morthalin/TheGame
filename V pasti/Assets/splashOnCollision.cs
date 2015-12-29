using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class splashOnCollision : MonoBehaviour {

	public ParticleSystem splashOriginal;
	public float cleanPeriod = 3.0f;
	public Quaternion q;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter (Collider other) {
		//Debug.Log("waterCollision");
		Quaternion p = new Quaternion();
		p.Set (0.0f, 0.0f, 0.0f, 0.0f);
		ParticleSystem tmp = Instantiate(splashOriginal, other.gameObject.transform.position,p) as ParticleSystem;
		tmp.gameObject.transform.Rotate(q.x,q.y,q.z);
		Destroy (tmp.gameObject, cleanPeriod);
	}
	// Update is called once per frame
	void Update () {

	}
}
