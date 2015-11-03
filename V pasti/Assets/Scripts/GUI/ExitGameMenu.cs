using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitGameMenu : MonoBehaviour {
	
	public Canvas mainMenu;
	public Canvas exitMenu;
	
	void Start () {
		mainMenu = mainMenu.GetComponent<Canvas> ();
		exitMenu = exitMenu.GetComponent<Canvas> ();
		exitMenu.enabled = true;
		mainMenu.enabled = false;
	}
	
	public void yesPressed()
	{
		Application.Quit ();
	}
	public void noPressed()
	{
		mainMenu.enabled = true;
		exitMenu.enabled = false;
	}
}