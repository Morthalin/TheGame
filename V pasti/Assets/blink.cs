using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {
	public ParticleSystem a;
	public ParticleSystem b;
	public float deltatime = 0.0f;
	private float startTime;
	public bool started = false;
	private bool startedA = false;
	private bool startedB = false;
	// Use this for initialization
	void Start () {
		a.Stop ();
		b.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (started && !startedA) {
			startTime = Time.time;
			startedA = true;
			a.Play();
		}
		if (Time.time - startTime > deltatime && !startedB && started) {
			b.Play ();
			startedB = true;
		}
		if (Time.time - startTime > 10.0f) {
			startedA = startedB = started = false;
		}
	}
}
