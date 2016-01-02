using UnityEngine;
using System.Collections;

public class DisappearOnStorypoint : MonoBehaviour {
	public int storypointToDisappear;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!pl) {
			Debug.LogError ("Missing player");
			return;
		}
		if (pl.storyCheckpoint == storypointToDisappear) {
			if( gameObject.activeSelf
			 && (transform.position-pl.transform.position).sqrMagnitude < 49f )
			{
				//Debug.Log("sud sebran.");
				gameObject.SetActive(false);
			}
		}
	}
}
