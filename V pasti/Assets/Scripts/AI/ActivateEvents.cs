using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateEvents : MonoBehaviour {
	//public bool dialog = true;
	public int dialogId;
	public int storyCheckpoint;
	public float distanceSqrtToActivate = 36f;
	private BasePlayer player  = null;
	private Text 	presseText = null;
	private bool    done = false;
    
	void Start () {
		player = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!player) {
			Debug.LogError("There is no player object.");
			return;
		}

		presseText = GameObject.Find ("Interface").transform.Find ("PressE").GetComponent<Text> ();
		if (presseText == null) {
			Debug.LogError("presseText");
			return;
		}
	}
	
	void Update () {
		// todo check storyCheckpoint for multiple takls
		if (! done && (transform.position -player.transform.position).sqrMagnitude < distanceSqrtToActivate) {
			done = true;
			//Debug.Log("nalezen " + transform.GetComponents<DialogEvent>().Length + " dialog(y).");
			gameObject.GetComponent<DialogEvent>().start = true;
		} 
	}

	void OnGUI()
	{
		if (! done && (transform.position - player.transform.position).sqrMagnitude < distanceSqrtToActivate) {
			GUI.TextField (new Rect (10, 10, 60, 30), "[t] dialog");
		}
	}
}
