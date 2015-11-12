using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform camera;

	void Start ()
    {
	    if(!camera)
        {
            Debug.LogError("Missing target transform!");
        }
	}
	
	void Update ()
    {
        transform.LookAt(camera);
	}
}
