using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitGameMenu : MonoBehaviour
{
	public Transform mainMenu;
	public Transform exitMenu;
	
	void Start ()
    {
        if (!mainMenu)
        {
            Debug.LogError("Missing main menu!");
        }

        if (!exitMenu)
        {
            Debug.LogError("Missing exit menu reference!");
        }
    }
	
	public void yesPressed()
	{
		Application.Quit ();
	}
	public void noPressed()
	{
        mainMenu.gameObject.SetActive(true);
        exitMenu.gameObject.SetActive(false);
    }
}