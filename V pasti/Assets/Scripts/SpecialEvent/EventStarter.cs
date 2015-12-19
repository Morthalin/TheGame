using UnityEngine;
using System.Collections;

public class EventStarter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Player" && transform.parent.GetComponent<EventController>().checkpoint == 0)
        {
            transform.parent.GetComponent<EventController>().checkpoint++;
        }
    }
}
