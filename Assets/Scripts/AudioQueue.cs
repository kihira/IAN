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
        audioQueue.Enqueue(clip);
        Debug.Log("Added audioSource to queue");
    }

    public void AddAudio(List<AudioClip> clips, bool forceTop = false)
    {
        if (forceTop)
        {
            AudioClip[] oldclips = new AudioClip[audioQueue.Count];
            audioQueue.CopyTo(oldclips, 0);

            audioQueue.Clear();
            foreach (AudioClip audioClip in clips)
            {
                AddAudio(audioClip);
            }
            foreach (AudioClip audioClip in oldclips)
            {
                AddAudio(audioClip);
            }
            // Force play top
            if (audioQueue.Count > 0) audioSource.clip = audioQueue.Dequeue();
            audioSource.Play();
        }
        else
        {
            foreach (AudioClip audioClip in clips)
            {
                AddAudio(audioClip);
            }
        }
    }
}
