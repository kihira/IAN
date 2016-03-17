using UnityEngine;
using System.Collections;

public class Vending : MonoBehaviour
{
    private float lastCollideTime = -1;
    private float cooldownTime = 2;

    private AudioQueue audioQueue;
    [SerializeField] private AudioClip audio;

    void Start()
    {
        audioQueue = GameObject.FindWithTag("Player").GetComponent<AudioQueue>();
    }
	
	void Update () {
        // Reset
	    if (lastCollideTime > -1f && Time.time - lastCollideTime > cooldownTime)
	    {
	        lastCollideTime = -1f;
	    }
	
	}

    HandModel GetHand(Transform other)
    {
        if (other == null) return null;
        return other.GetComponent<HandModel>() ?? GetHand(other.transform.parent);
    }

    void OnTriggerEnter(Collider other)
    {
        HandModel hand = GetHand(other.transform);
        //if (hand == null) return;

        if (lastCollideTime <= 0f)
        {
            lastCollideTime = Time.time;
            audioQueue.AddAudio(audio);
        }
    }
}
