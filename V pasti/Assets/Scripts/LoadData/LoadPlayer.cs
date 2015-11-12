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
        //GameObject.Find("knight").GetComponent<BaseNPC>().LoadNPC("Knight");
        //Cursor.visible = false;
    }
}
