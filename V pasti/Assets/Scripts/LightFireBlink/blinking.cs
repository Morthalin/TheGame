using UnityEngine;
using System.Collections;

public class blinking : MonoBehaviour {
	public float duration = 1.0F;
	public Light lt;
	public float minIntensity = 4.0f;
	public float maxIntensity = 6.0f;
	public float deltaTime = 0.05f;
	private float lastTime = 0.0f;

	void Start() {
		lt = GetComponent<Light>();
	}
	void Update() {
		float T = Time.time;
		if (T - lastTime > deltaTime) {
			lastTime = T;
			lt.intensity += Random.Range(minIntensity/2, maxIntensity/2) * (Random.Range(0.0f,1.0f) > 0.5 ? 1 : -1) ;
			if(lt.intensity < minIntensity){
				lt.intensity = minIntensity;
			}
			if(lt.intensity > maxIntensity){
				lt.intensity = maxIntensity;
			}
			Color c = new Color();
			c.r = 1.0f;
			c.g = Random.Range(110.0f/255.0f, 200.0f/255.0f);
			c.b = 52.0f/255.0f;
			c.a = 1.0f;
			lt.color = c;
		}

		//float phi = Time.time / duration * 2 * Mathf.PI;
		//float amplitude = Mathf.Cos(phi) * 0.5F /*+ 0.5F*/;
		//lt.intensity += amplitude;
	}
}