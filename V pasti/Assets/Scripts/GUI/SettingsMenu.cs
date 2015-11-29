using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
	public Transform mainMenu;
	public Transform settingsMenu;
    
	void Start ()
    {
        if (!mainMenu)
        {
            Debug.LogError("Missing main menu!");
        }

        if (!settingsMenu)
        {
            Debug.LogError("Missing setting menu reference!");
        }
    }
	
	void Update ()
    {
		
	}

	public void navratPressed()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }
}
