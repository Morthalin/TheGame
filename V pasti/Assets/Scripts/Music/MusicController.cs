using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;

	void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
        {
            Debug.LogError("Missing audio source!");
        }

        if(clips.Length == 0)
        {
            Debug.LogError("None music for play!");
        }
	}
	
	void Update ()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
	}
}
