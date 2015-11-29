using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadPlayerChar : MonoBehaviour
{
    public string jmeno = "";

	void Awake ()
    {
        DontDestroyOnLoad(this);
    }
}
