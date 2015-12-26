using UnityEngine;
using System.Collections;

public class dayNight : MonoBehaviour {
	public Material sb;
	public float rotSpeed = 1.0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			float newRot = RenderSettings.skybox.GetFloat ("_Rotation");
			newRot += rotSpeed;
			if (newRot > 360.0f) {
				newRot -= 360.0f;
			}
			RenderSettings.skybox.SetFloat("_Rotation", newRot);
		//}
	}
}
