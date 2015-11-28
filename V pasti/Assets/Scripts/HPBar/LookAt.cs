using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform cam;

	void Start ()
    {
	    if(!cam)
        {
            cam = GameObject.Find("Player").transform.FindChild("Camera Target").FindChild("Main Camera");
            if(!cam)
            {
                Debug.LogError("Missing main camera!");
            }
        }
	}
	
	void Update ()
    {
        transform.LookAt(cam);
	}
}
