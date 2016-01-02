using UnityEngine;
using System.Collections;

public class SpecialStoryJumps : MonoBehaviour {
//	public float welcomeFightDuration = 4.0f;
	public int   MageStoryPoint = 23;

//	private float welcomeFightSpend = 1.0f;
//	private bool welcomeFight = false;

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
		if (pl.storyCheckpoint == MageStoryPoint) {
			if (4 * GameObject.Find("Mages_cave").transform.Find ("mage").transform.GetComponent<BaseNPC>().health
			    <= GameObject.Find("Mages_cave").transform.Find ("mage").transform.GetComponent<BaseNPC>().healthMax) {
				//welcomeFightSpend -= Time.deltaTime;
				pl.storyCheckpoint ++;
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<BaseNPC>().inCombat = false;
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("combat", false);
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("running", false);
			}
		}
		// vycaruje sud
		else if (pl.storyCheckpoint == MageStoryPoint + 2) {
			/// cary mary
			GameObject sud = GameObject.Find ("Mages_cave").transform.Find("sud").gameObject;
			if(!sud) {
				Debug.LogError("Soudek neni");
				return;
			}
			sud.SetActive(true);
			pl.storyCheckpoint +=2;
		}
		// vycaruje portal
		else if (pl.storyCheckpoint == 28) {
			/// cary mary
			GameObject portal = GameObject.Find ("Mages_cave").transform.Find("Portal").gameObject;
			if(!portal) {
				Debug.LogError("Portal neni");
				return;
			}
			portal.SetActive(true);
		}
	}
}
