using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateEvents : MonoBehaviour {
	public bool dialog = true;
	public int dialogId;
	public int storyCheckpoint;
	public float distanceSqrtToActivate = 49f;
	public float distanceSqrtToGo = 196f;
	public Type type = Type.DIALOG;
	private BasePlayer player  = null;
//	private Text 	presseText = null;
	private bool    done = false;
	private NavMeshAgent navigation;
	private bool isRunning = false;
    

	public enum Type
	{
		DIALOG,
		MONOLOG,
	}

	void Start () {
		player = GameObject.Find ("Player").GetComponent<BasePlayer> ();
		if (!player) {
			Debug.LogError("There is no player object.");
			return;
		}
		if (type == Type.MONOLOG)
			return;

		navigation = transform.GetComponent<NavMeshAgent>();
		if(!navigation)
		{
			Debug.LogError("Missing NavMeshAgent!");
			return;
		}

/*		presseText = GameObject.Find ("Interface").transform.Find ("PressE").GetComponent<Text> ();
		if (presseText == null) {
			Debug.LogError("presseText");
			return;
		}*/
	}
	
	void Update () {
		// todo check storyCheckpoint for multiple takls
		if (storyCheckpoint != player.storyCheckpoint || done )
			return;

		if (type == Type.DIALOG) {
			dialogUpdate ();
		} else if (type == Type.MONOLOG) {
			monologUpdate ();
		}
	}

	void dialogUpdate()
	{
		if ( (transform.position -player.transform.position).sqrMagnitude < distanceSqrtToGo ) {

			transform.LookAt(player.transform.position);
			isRunning = true;
			if(transform.name == "mage"){
			// mag se dela pres animator
				GetComponent<Animator>().SetBool("combat", false);
				GetComponent<Animator>().SetBool("running", false);
			} else {
			// ostatni pres nav
				GetComponent<Animator>().SetBool("isRunning",true);
				GetComponent<Animator>().SetBool("runningForward",true);
                navigation.SetDestination(player.transform.position - transform.forward.normalized * Mathf.Sqrt(distanceSqrtToActivate - 10.0f));
            }

            monologUpdate();
			if( done ){
				player.transform.LookAt(transform.position);
				if(transform.name == "mage"){
					// mag se dela pres animator
					GetComponent<Animator>().SetBool("combat", false);
					GetComponent<Animator>().SetBool("running", false);
				} else {
					// ostatni pres nav
					GetComponent<Animator>().SetBool("isRunning",false);
					GetComponent<Animator>().SetBool("runningForward",false);
				}
			}
		} else if(isRunning){
			isRunning = false;
			if(transform.name == "mage"){
				// mag se dela pres animator
				//GetComponent<Animator>().SetBool("combat", false);
				//GetComponent<Animator>().SetBool("running", false);
			} else {
				// ostatni pres navMeshAgent
				GetComponent<Animator>().SetBool("isRunning",false);
				GetComponent<Animator>().SetBool("runningForward",false);
			}
		}
	}

	void monologUpdate()
	{
		if ((transform.position -player.transform.position).sqrMagnitude < distanceSqrtToActivate) {
			foreach(var item in gameObject.GetComponents<DialogEvent>()){
				if( item.dialog == dialogId ){
					item.start = true;
					done = true;
				}
			}
		}
	}

/*	void OnGUI()
	{
		if (! done && (transform.position - player.transform.position).sqrMagnitude < distanceSqrtToActivate) {
			GUI.TextField (new Rect (10, 10, 60, 30), "[t] dialog");
		}
	}*/
}
