using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas settingsMenu;

	// Use this for initialization
	void Start () {
		mainMenu = mainMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		settingsMenu.enabled = true;
		mainMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// todo tahla
	}

	// todo bind funkce
}
