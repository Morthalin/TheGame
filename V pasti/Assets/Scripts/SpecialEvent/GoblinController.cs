using UnityEngine;
using System.Collections;

public class GoblinController : MonoBehaviour
{
    private NavMeshAgent agent;

	void Awake ()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        if(!agent)
        {
            Debug.LogError("Missing NavMeshAgent!");
        }
	}
	
	void Update ()
    {
	    if(agent.remainingDistance != 0f)
        {
            transform.GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            transform.GetComponent<Animator>().SetBool("isRunning", false);
        }
	}
}
