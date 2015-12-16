using UnityEngine;
using System.Collections;

public class resetPositionIfAltitude : MonoBehaviour {
		
	public Vector3 StartPosition;
	public float height;

	// Use this for initialization
	void Start () {
		StartPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < /*StartPosition.y -*/ height) {
			gameObject.transform.position = StartPosition;
			gameObject.GetComponent<Rigidbody>().isKinematic = !gameObject.GetComponent<Rigidbody>().isKinematic;
			gameObject.GetComponent<Rigidbody>().isKinematic = !gameObject.GetComponent<Rigidbody>().isKinematic;
		}
	}
}
