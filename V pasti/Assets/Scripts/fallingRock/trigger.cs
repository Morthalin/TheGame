using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour {

	public GameObject Player;
	public float resetTime = 10.0f;
	public GameObject[] Stones;
	public float [] DeltaTimes;
	public Vector3[] stonePosition;


	private bool isTriggerd = false;
	private float startTime;
	private int pos = 0;
	private float lastIteration;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Collider> ().isTrigger = true;
		//stonePosition = new Vector3[Stones.Length];
		stonePosition.Initialize ();

		for (int i = 0; i < Stones.Length; i++){
			Physics.IgnoreCollision (Stones [i].GetComponent<CapsuleCollider> (), 
			                         Player.GetComponent<CharacterController> ().GetComponent<Collider>());
			stonePosition[i] = Stones[i].transform.position;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			isTriggerd = true;
			startTime = Time.time;
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

	void restart(){
		if (lastIteration + resetTime < Time.time) {
			for(int i = 0; i < Stones.Length;i++){
				Stones[i].transform.position = stonePosition[i];
				if( ((int)((lastIteration-startTime)/resetTime))%3 == 0){
					Stones[i].GetComponent<Rigidbody>().isKinematic = true;
					Stones[i].GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			lastIteration = Time.time;
		}
	}

	// Update is called once per frame
	void Update () {
		if (isTriggerd) {
			drop ();
		} 
		if (pos > 0) {
			restart ();
		}


	}
}
