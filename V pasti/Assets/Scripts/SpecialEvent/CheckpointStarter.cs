using UnityEngine;
using System.Collections;

public class CheckpointStarter : MonoBehaviour
{
    public GameObject goblin1;
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Player" && transform.parent.GetComponent<EventController>().checkpoint == 3)
        {
            transform.parent.GetComponent<EventController>().checkpoint++;
        }

        if(collider.gameObject.name == "Goblin" && transform.parent.GetComponent<EventController>().checkpoint == 2)
        {
            collider.gameObject.SetActive(false);
            goblin1.SetActive(true);
            transform.parent.GetComponent<EventController>().checkpoint++;
        }
    }   
}
