using UnityEngine;
using System.Collections;

public class SpecialStoryJumps : MonoBehaviour {
	public float welcomeFightDuration = 4.0f;
	public int   MageStoryPoint = 23;

	private float welcomeFightSpend = 1.0f;
	private bool welcomeFight = false;

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
			if (GameObject.Find("Mages_cave").transform.Find ("mage").transform.GetComponent<BaseNPC>().inCombat) {
				welcomeFightSpend -= Time.deltaTime;
				if(!welcomeFight)
				{
					welcomeFight = true;
					welcomeFightSpend = welcomeFightDuration;
				}
				if(welcomeFightSpend < 0.0f)
				{
					pl.storyCheckpoint ++;
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<BaseNPC>().inCombat = false;
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("combat", false);
				//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("running", false);
				}
			} else {
				welcomeFight = false;
			}
		}
		// vycaruje sud
		else if (pl.storyCheckpoint == MageStoryPoint + 2) {
			/// cary mary
			GameObject sud = GameObject.Find ("Mages_cave").transform.Find("soudek").gameObject;
			if(!sud) {
				Debug.LogError("Soudek neni");
				return;
			}
			sud.transform.GetChild(0).GetComponent<Transform>().position = -2.0f*pl.transform.right +
				.5f*(pl.transform.position + GameObject.Find ("Mages_cave").transform.Find ("mage").transform.position); 
			sud.transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(1.0f,1.0f,1.0f);
			pl.storyCheckpoint +=2;
		}
		// vycaruje portal
		else if (pl.storyCheckpoint == 28) {
			/// cary mary
			GameObject portal = GameObject.Find ("Mages_cave").transform.Find("Portal").gameObject;
			if(!portal) {
				Debug.LogError("Soudek neni");
				return;
			}
			portal.transform.GetComponent<Transform>().position = 4.0f*pl.transform.right +
				.5f*(pl.transform.position + GameObject.Find ("Mages_cave").transform.Find ("mage").transform.position); 
			portal.SetActive(true);
		}
	}
}
