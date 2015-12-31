using UnityEngine;
using System.Collections;

public class EventStarter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Player" && collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint == 9)
        {
            collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint++;
        }
    }
}
