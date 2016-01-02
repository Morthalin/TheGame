using UnityEngine;
using System.Collections;

public class RunOnInitPosition : MonoBehaviour
{
    public int storyCheckpoint;
    private Vector3 initPosition;
    private Quaternion initRotation;
    private NavMeshAgent agent;
    bool done;

    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
        agent = transform.GetComponent<NavMeshAgent>();
        if (!agent)
        {
            Debug.LogError("Missing NavMeshAgent!");
        }
        done = false;
	}
	
	void Update ()
    {
	    if(storyCheckpoint == GameObject.Find("Player").GetComponent<BasePlayer>().storyCheckpoint && !done)
        {
            agent.SetDestination(initPosition);
            done = true;
        }

        if(agent.remainingDistance == 0 && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            transform.rotation = initRotation;
        }
	}
}
