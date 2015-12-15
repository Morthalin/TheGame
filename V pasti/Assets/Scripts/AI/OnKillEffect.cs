using UnityEngine;
using System.Collections;

public class OnKillEffect : MonoBehaviour {
	public enum EffectType{
		STORYJUMP = 1,
	}


	BaseNPC npc = null;
	BasePlayer player = null;
	public EffectType effectType;
	public int        targetStoryCheckpoint;

	// Use this for initialization
	void Start () {
		npc = transform.GetComponent<BaseNPC> ();
		if (!npc) {
			Debug.LogError("This transform has not BaseNPC component.");
			return;
		}
		player = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!player) {
			Debug.LogError("There is no player object.");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (npc.health <= 0) {
			if(effectType == EffectType.STORYJUMP){
				player.storyCheckpoint = targetStoryCheckpoint;
				return;
			}
		}
	}
}
