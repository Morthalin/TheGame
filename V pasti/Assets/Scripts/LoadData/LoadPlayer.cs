using UnityEngine;
using System.Collections;

public class LoadPlayer : MonoBehaviour
{
    private BasePlayer player;
    private BaseNPC baseNPC;
    public GameObject loadPlayerCharacter;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<BasePlayer>();
        if (!GameObject.Find("LoadPlayer"))
        {
            GameObject playerCharacter;
            playerCharacter = Instantiate(loadPlayerCharacter);
            playerCharacter.name = "LoadPlayer";
            playerCharacter.GetComponent<LoadPlayerChar>().jmeno = "tester";
		}

        player.LoadPlayer(GameObject.Find("LoadPlayer").GetComponent<LoadPlayerChar>().jmeno);
    }

    public void loadPlayer(string name)
    {
        player.LoadPlayer(name);
    }
}
