using UnityEngine;
using System.Collections;

public class LoadPlayer : MonoBehaviour
{
    private BasePlayer player;
    private BaseNPC baseNPC;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<BasePlayer>();
        player.LoadPlayer("Morth");
        baseNPC = GameObject.Find("Knight").GetComponent<BaseNPC>();
        baseNPC.LoadNPC("Knight");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Player").GetComponent<BasePlayer>().pause == false)
        {
            GameObject.Find("Player").GetComponent<BasePlayer>().pause = true;
            //Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("Player").GetComponent<BasePlayer>().pause == true)
        {
            GameObject.Find("Player").GetComponent<BasePlayer>().pause = false;
        }
    }
}
