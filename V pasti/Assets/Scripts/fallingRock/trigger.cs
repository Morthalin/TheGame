using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider))]
public class trigger : MonoBehaviour {

	public GameObject Player;
	public float resetTime = 10.0f;
	public GameObject[] Stones;
	public float [] DeltaTimes;
	//public Vector3[] stonePosition;


	private bool isTriggerd = false;
	private float startTime;
	private int pos = 0;
	private float lastIteration;
	private MeshRenderer[] renderers;

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<Collider> ().isTrigger = true;
		/*stonePosition = new Vector3[Stones.Length];
		stonePosition.Initialize ();*/

		renderers = GetComponentsInChildren<MeshRenderer> ();

		for (int i = 0; i < Stones.Length; i++){
			Physics.IgnoreCollision (Stones [i].GetComponent<CapsuleCollider> (), 
			                         Player.GetComponent<CharacterController> ().GetComponent<Collider>());
			//stonePosition[i] = Stones[i].transform.position;
		}
	}

	void OnTriggerEnter(Collider other) {
		foreach (MeshRenderer mr in renderers) {
			mr.enabled = true;
		}
		if (other.gameObject.name == "Player") {
			isTriggerd = true;
			startTime = Time.time;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.name == "Player") {
			foreach (MeshRenderer mr in renderers) {
				mr.enabled = false;
			}
		}
	}

	void drop(){
		if (pos < Stones.Length ) {
			if (startTime + DeltaTimes [pos] <= Time.time) {
				Stones [pos].GetComponent<Rigidbody> ().isKinematic = false;
				pos++;
			}
		} else {
			isTriggerd = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (isTriggerd) {
			drop ();
		} 
	}
}
