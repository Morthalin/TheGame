using UnityEngine;
using System.Collections;

public class BridgeDropStarter : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint == 14)
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            collider.gameObject.GetComponent<BasePlayer>().storyCheckpoint++;
        }
    }
}
