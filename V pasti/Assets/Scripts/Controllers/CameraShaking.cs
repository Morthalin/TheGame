using UnityEngine;
using System.Collections;

public class CameraShaking : MonoBehaviour
{
    public bool shaking = false;

	void Awake ()
    {
        shaking = false;
	}
	
	void Update ()
    {
        if(shaking)
            transform.position += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
	}
}
