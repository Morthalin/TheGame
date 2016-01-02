using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Choice : MonoBehaviour {

	// Use this for initialization
	float oldTimeScale;
	void Start () {
		GameObject.Find ("Player").GetComponent<BasePlayer> ().pause++;
		oldTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	// show it by: GameObject.Find ("Interface").transform.Find ("Choice").gameObject.SetActive (true);
	public void V1_onClick(){
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!pl) {
			Debug.LogError ("Missing player");
			return;
		}
		pl.storyCheckpoint = 28;
		GameObject.Find ("Interface").transform.Find ("Choice").gameObject.SetActive (false);
	//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("combat", false);
	//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("running", false);
		GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
		Time.timeScale = oldTimeScale;
	}

	public void V2_onClick(){
		BasePlayer pl = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!pl) {
			Debug.LogError ("Missing player");
			return;
		}
		pl.storyCheckpoint = 30;
		GameObject.Find ("Interface").transform.Find ("Choice").gameObject.SetActive (false);
	//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("combat", true);
	//	GameObject.Find ("Mages_cave").transform.Find ("mage").GetComponent<Animator>().SetBool("running", true);
	//	GameObject.Find ("Mages_cave").transform.Find ("mage").transform.GetComponent<BaseNPC> ().inCombat = true;
		GameObject.Find("Player").GetComponent<BasePlayer>().pause--;
		Time.timeScale = oldTimeScale;
	}
}
