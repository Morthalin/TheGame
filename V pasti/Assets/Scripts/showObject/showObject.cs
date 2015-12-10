using UnityEngine;
using System.Collections;

public class showObject : MonoBehaviour {
	// Use this for initialization
	public GameObject [] objects;
	SphereCollider sc;
	private MeshRenderer[] renderers;
	private SkinnedMeshRenderer[] skinnedRenderers;
	private Canvas[] hpBars;

	void Start () {

		foreach (GameObject go in objects){
			go.SetActive (true);
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
	}

	// Update is called once per frame
	void Update () {
	
	}
}
