using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestInfo : MonoBehaviour {
	BasePlayer player;
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
		ttl = 6.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (last < player.storyCheckpoint) {
			ttl = 6.0f;
			panel.gameObject.SetActive(true);
			text.enabled = true;
			last = player.storyCheckpoint;
		}
		switch (player.storyCheckpoint) {
		case 1: text.text = "<b>Ukol 1:</b> Vydej se po cestě do místní vesnice, musíš sehnat vyproštovák.";
			break;
		case 2: text.text = "<b>Info:</b> Podle pradávného proroctví, kdo nezemře po vypití nápoje který jsi právě vypil, je vyvolený.";
			break;
		case 3: text.text = "<b>Nápověda:</b> Hledej domorodce, ktery s tebou chce mluvit.";
			break;
		case 4: text.text = "<b>Nápověda:</b> Mistní bylinkářka má pro tebe prosbu.";
			break;
		case 5: text.text = "<b>Ukol 2:</b> Zabij bandity a přines domorodci ukradené suroviny.";
			break;
		case 7: text.text = "<b>Info:</b> Výborně, získal jsi zpět věci paní bylinářky, která na tebe s napětím čeká.";
			break;
		default: text.text = "";
			panel.gameObject.SetActive(false);
			text.enabled = false;
			break;
		}
		if (ttl > 0.0f) {
			ttl -= Time.deltaTime;
			if( ttl <= 0.0f ){
				panel.gameObject.SetActive(false);
				text.enabled = false;
				applyPostMessageEffect();
			}
		}

		if(Input.GetKeyDown(KeyCode.L)){
			last --;
		}
	}


	void applyPostMessageEffect(){
		if (player.storyCheckpoint == 2) {
			player.storyCheckpoint = 3;
		} else if (player.storyCheckpoint == 4) {
		}
	}
}
