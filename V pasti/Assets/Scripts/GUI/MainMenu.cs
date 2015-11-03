using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas newMenu;
	public Canvas loadMenu;
	public Canvas settingsMenu;
	public Canvas exitMenu;

    void Start () {
		mainMenu = mainMenu.GetComponent<Canvas> ();
		newMenu = newMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		loadMenu = loadMenu.GetComponent<Canvas> ();
		exitMenu = exitMenu.GetComponent<Canvas> ();
		mainMenu.enabled = true;
		newMenu.enabled = settingsMenu.enabled = loadMenu.enabled = exitMenu.enabled = false;
    }
	
	public void newGamePressed()
	{
		mainMenu.enabled = false;
		newMenu.enabled = true;
	}
	public void loadGamePressed()
	{
		mainMenu.enabled = false;
		loadMenu.enabled = true;
	}
	public void settingsPressed()
	{
		mainMenu.enabled = false;
		settingsMenu.enabled = true;
	}
	public void exitGamePressed()
	{
		mainMenu.enabled = false;
		exitMenu.enabled = true;
	}
}
