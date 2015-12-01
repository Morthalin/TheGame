using UnityEngine;
using System.Collections;

public class LoadingSceen : MonoBehaviour
{
    public Transform loadingScreen;
    public int loading = 0;
	void Start ()
    {
        if(!loadingScreen)
        {
            Debug.LogError("Missing Loading screen!");
        }
        loading = 0;
	}
	
	void Update ()
    {
	    if(loading > 0)
        {
            loadingScreen.gameObject.SetActive(true);
        }
        else
        {
            loadingScreen.gameObject.SetActive(false);
        }
	}
}
