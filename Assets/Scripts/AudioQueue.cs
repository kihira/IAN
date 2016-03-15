using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioQueue : MonoBehaviour
{
    private readonly Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }
	
	void Update () {
	    if (!audioSource.isPlaying && audioQueue.Count > 0)
	    {
            Debug.Log("Playing next clip");
            audioSource.clip = audioQueue.Dequeue();
            audioSource.Play();
	    }
	}

    public void AddAudio(AudioClip clip)
    {
        Debug.Log("Added audioSource to queue");
        audioQueue.Enqueue(clip);
    }
}
