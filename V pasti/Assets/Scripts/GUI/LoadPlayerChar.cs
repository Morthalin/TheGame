using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadPlayerChar : MonoBehaviour
{
    public string name = "";

	void Awake ()
    {
        DontDestroyOnLoad(this);
    }
}
