using UnityEngine;
using System.Collections;

public class SpeakTrigger : MonoBehaviour {

	public AudioClip voiceClip;
    private AudioSource audioSource;
	private bool hasPlayed = false;

	void Start () {
        audioSource = GameObject.Find("Player/Hand Mount").GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("The Trigger functions");
		if (!hasPlayed) {
			Debug.Log ("Audio hasn't been played");
		    audioSource.clip = voiceClip;
            audioSource.Play();
            Destroy(this.gameObject);
		}
	}
}
