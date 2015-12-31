using UnityEngine;
using System.Collections;

public class CheckpointStarter : MonoBehaviour
{
    public GameObject goblin1;
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Player" && collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint == 12)
        {
            collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint++;
        }

        if(collider.gameObject.name == "Goblin" && GameObject.Find("Player").GetComponent<BasePlayer>().storyCheckpoint == 11)
        {
            collider.gameObject.SetActive(false);
            goblin1.SetActive(true);
            GameObject.Find("Player").GetComponent<BasePlayer>().storyCheckpoint++;
        }
    }   
}
