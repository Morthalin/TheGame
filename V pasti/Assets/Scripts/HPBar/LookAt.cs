using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform cam;

	void Start ()
    {
	    if(!cam)
        {
            Debug.LogError("Missing target transform!");
        }
	}
	
	void Update ()
    {
        transform.LookAt(cam);
	}
}
