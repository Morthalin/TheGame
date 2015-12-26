using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadPlayerChar : MonoBehaviour
{
    public string jmeno = "";
	public float generalVolume;
	public float ambientVolume;
	public float effectsVolume;
	public bool mute;
	void Awake ()
    {
        DontDestroyOnLoad(this);

    }
}
