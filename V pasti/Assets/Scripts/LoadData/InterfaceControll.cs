using UnityEngine;
using System.Collections;

public class InterfaceControll : MonoBehaviour
{
    public Transform menu;

	void Awake ()
    {
        if(!menu)
        {
            Debug.LogError("Missing menu prefab!");
        }
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.gameObject.activeSelf)
        {
            menu.gameObject.SetActive(true);
            menu.GetChild(1).gameObject.SetActive(true);
            menu.GetChild(2).gameObject.SetActive(false);
            menu.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<BasePlayer>().pause++;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.gameObject.activeSelf)
        {
            menu.gameObject.SetActive(false);
            menu.GetChild(1).gameObject.SetActive(false);
            menu.GetChild(2).gameObject.SetActive(false);
            menu.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
        }
    }
}
