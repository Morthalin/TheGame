using UnityEngine;
using System.Collections;

public class LoadPlayer : MonoBehaviour
{
    private BasePlayer player;
    private BaseNPC baseNPC;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<BasePlayer>();
        if (GameObject.Find("LoadPlayer"))
        {
            player.LoadPlayer(GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().jmeno);
        }
        else
        {
            player.LoadPlayer("TestEvent");
        }
    }

    public void loadPlayer(string name)
    {
        player.LoadPlayer(name);
    }
}
