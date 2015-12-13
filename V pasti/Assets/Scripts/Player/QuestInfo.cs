using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestInfo : MonoBehaviour {
	BasePlayer player;
	bool 	seen;
	float 	ttl;
	int 	last;

	Text    	  text;
	RectTransform panel;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!player) {
			Debug.LogError("There is no player object.");
			return;
		}

		panel = GameObject.Find ("Interface").transform.Find ("QuestInfoPan").GetComponent<RectTransform> ();
		if (panel == null) {
			Debug.LogError("QuestInfoPan not found.");
			return;
		}
		text = panel.transform.GetComponentInChildren<Text> ();
		if (text == null) {
			Debug.LogError ("QuestInfoText");
			return;
		}
		last = -1;
		ttl = 10.0f;
		seen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (last < player.storyCheckpoint) {
			ttl = 10.0f;
			panel.gameObject.SetActive(true);
			text.enabled = true;
			last = player.storyCheckpoint;
		}
		switch (player.storyCheckpoint) {
		case 0: text.text = "Vydej se po cestě do místní vesnice, musíš sehnat vyproštovák.";
			break;
		case 1: text.text = "Druha zprava";
			break;
		case 2: text.text = "Treti zprava.";
			break;
	
		default: text.text = "";
			break;
		}
		if (ttl > 0.0f) {
			ttl -= Time.deltaTime;
			if( ttl <= 0.0f ){
				panel.gameObject.SetActive(false);
				text.enabled = false;
			}
		}
	}

}
