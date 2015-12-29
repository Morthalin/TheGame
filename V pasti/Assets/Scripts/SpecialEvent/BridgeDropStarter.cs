using UnityEngine;
using System.Collections;

public class BridgeDropStarter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && transform.parent.GetComponent<EventController>().checkpoint == 5)
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            transform.parent.GetComponent<EventController>().checkpoint++;
        }
    }
}
