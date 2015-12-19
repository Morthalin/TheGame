using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour
{
    public int checkpoint;

	void Start ()
    {
        checkpoint = 0;
	}
	
	void Update ()
    {
	    switch(checkpoint)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
	}
}
