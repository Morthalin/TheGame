﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoTable : MonoBehaviour {
	public bool isActive = false;
	private Transform statPanel = null;  
	// Use this for initialization
	void Start () {
		statPanel = GameObject.Find ("Interface").transform.Find("InfoTable");
	
		if (!statPanel) {
			Debug.LogError ("InfoTable has not been loaded from Interface.");
			return;
		}
		statPanel.gameObject.SetActive(isActive);
	}
	
	// Update is called once per frame
	void Update () {
		// zobrazi / skryje statistiky hrace

        if(GameObject.Find("Player").GetComponent<BasePlayer>().pause == 0 && isActive)
        {
            isActive = false;
            statPanel.gameObject.SetActive(isActive);
        }

		if (Input.GetKeyDown ("c") || Input.GetKeyDown("i")) {
			isActive = !isActive;
			statPanel.gameObject.SetActive(isActive);
            if(isActive)
                GameObject.Find("Player").GetComponent<BasePlayer>().pause++;
            else
                GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
        }
		// pokud jsou staty aktivni updatujeme hodnoty
		if (isActive) {
			BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();

			statPanel.FindChild("ClassName").Find("Value").GetComponent<Text>().text
				= pl.playerClass.CharacterClassName;
			statPanel.FindChild("Strength").Find("Value").GetComponent<Text>().text = "" + pl.strength;
			statPanel.FindChild("Intellect").Find("Value").GetComponent<Text>().text = "" + pl.intellect;
			statPanel.FindChild("Agility").Find("Value").GetComponent<Text>().text = "" + pl.agility;
			statPanel.FindChild("Stamina").Find("Value").GetComponent<Text>().text = "" + pl.stamina;
			statPanel.FindChild("Energy").Find("Value").GetComponent<Text>().text = "" + pl.energy;
			statPanel.FindChild("Armor").Find("Value").GetComponent<Text>().text = "" + pl.armor;
			statPanel.FindChild("Attack").Find("Value").GetComponent<Text>().text = "" + (pl.maxAttack+pl.maxAttack)/2;
			//statPanel.GetChild(0).GetChild(1).GetComponent<Text>().text = "8";
		}
	}
}
