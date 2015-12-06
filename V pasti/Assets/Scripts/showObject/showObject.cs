using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SphereCollider))]


public class showObject : MonoBehaviour {

	// Use this for initialization
	public GameObject NPCs;
	public GameObject items;
	SphereCollider sc;
	private MeshRenderer[] renderers;
	private SkinnedMeshRenderer[] skinnedRenderers;
	private Canvas[] hpBars;

	void Start () {
		sc = GetComponent<SphereCollider>();
		sc.isTrigger = true;
		sc.radius = 100.0f;

		if (NPCs) {
			NPCs.SetActive (true);
			}
		if (items) {
			items.SetActive (true);
		}

		renderers = GetComponentsInChildren<MeshRenderer> ();
		skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer> ();
		hpBars = GetComponentsInChildren<Canvas> ();
		foreach (MeshRenderer renderer in renderers) {
			renderer.enabled = false;
		}
		foreach (SkinnedMeshRenderer renderer in skinnedRenderers) {
			renderer.enabled = false;
		}
		foreach (Canvas canvas in hpBars) {
			canvas.gameObject.SetActive(false);
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.name == "Player") {
			foreach (MeshRenderer renderer in renderers) {
				renderer.enabled = false;
			}
			foreach (SkinnedMeshRenderer renderer in skinnedRenderers) {
				renderer.enabled = false;
			}
			foreach (Canvas canvas in hpBars) {
				canvas.gameObject.SetActive(false);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				
				items.SetActive (false);
			}
		}
		/*
		if (NPCs) {
			if (other.gameObject.name == "Player") {
				NPCs.SetActive (false);
			}
		}

		*/
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.name == "Player") {
			foreach (MeshRenderer renderer in renderers) {
				renderer.enabled = true;
			}
			foreach (SkinnedMeshRenderer renderer in skinnedRenderers) {
				renderer.enabled = true;
			}
			foreach (Canvas canvas in hpBars) {
				canvas.gameObject.SetActive(true);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				//Debug.Log ("Enter");
				items.SetActive (true);
			}
		}
		/*if (NPCs) {
			if (other.gameObject.name == "Player") {
				//Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}

		*/
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			foreach (MeshRenderer renderer in renderers) {
				renderer.enabled = true;
			}
			foreach (SkinnedMeshRenderer renderer in skinnedRenderers) {
				renderer.enabled = true;
			}
			foreach (Canvas canvas in hpBars) {
				canvas.gameObject.SetActive(true);
			}
		}
		if (items) {
			if (other.gameObject.name == "Player") {
				//Debug.Log ("Enter");
				items.SetActive (true);
			}
		}
		/*
		if (NPCs) {
			if (other.gameObject.name == "Player") {
				//Debug.Log ("Enter");
				NPCs.SetActive (true);
			}
		}


		 */
	}

	// Update is called once per frame
	void Update () {
	
	}
}
