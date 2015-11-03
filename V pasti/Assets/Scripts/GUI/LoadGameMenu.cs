using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGameMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas loadGameMenu;
	
	// Use this for initialization
	void Start () {
		mainMenu = mainMenu.GetComponent<Canvas> ();
		loadGameMenu = loadGameMenu.GetComponent<Canvas> ();
		loadGameMenu.enabled = true;
		mainMenu.enabled = false;

		// todo load saves from db
	}

	// todo bind function with load level
}
