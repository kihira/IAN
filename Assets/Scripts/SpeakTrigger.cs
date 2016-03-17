using UnityEngine;
using System.Collections;

public class SpeakTrigger : MonoBehaviour {

	public AudioClip voiceClip;
    private AudioQueue audioQueue;
	private bool hasPlayed = false;

	void Start () {
        audioQueue = GameObject.Find("/Player/Hand Mount").GetComponent<AudioQueue>();
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("The Trigger functions");
		if (!hasPlayed) {
			Debug.Log ("Audio hasn't been played");
            audioQueue.AddAudio(voiceClip);
            Destroy(this.gameObject);
		}
	}
}
