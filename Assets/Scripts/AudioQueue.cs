using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioQueue : MonoBehaviour
{
    private readonly Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
	
	void Update () {
	    if (!audio.isPlaying && audioQueue.Count > 0)
	    {
            Debug.Log("Playing next clip");
            audio.clip = audioQueue.Dequeue();
            audio.Play();
	    }
	}

    public void AddAudio(AudioClip clip)
    {
        Debug.Log("Added audio to queue");
        audioQueue.Enqueue(clip);
    }
}
