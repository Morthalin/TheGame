using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewGameMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas newGameMenu;
	public string playerName;

	void Start () {
		mainMenu = mainMenu.GetComponent<Canvas> ();
		newGameMenu = newGameMenu.GetComponent<Canvas> ();
		newGameMenu.enabled = true;
		mainMenu.enabled = false;
	}
	
	// todo bind function with load level
	void PlayPressed()
	{
		if (playerName.Length != 0) {
			// Load level
		} else {
			// jmeno
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
