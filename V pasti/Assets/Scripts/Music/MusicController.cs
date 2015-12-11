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
	}
	
	void Update ()
    {
        if(!audioSource.isPlaying && clips.Length != 0)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
	}
}
