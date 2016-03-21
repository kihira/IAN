using UnityEngine;
using System.Collections;

public class ServerRoomTrigger : MonoBehaviour {

    public AudioSource source;
    public AudioClip clips;

    public void Awake(){
        source = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine("fadeIn");
    }

    public void OnTriggerExit(Collider other)
    {
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut()
    {
        while(source.volume > 0.01f)
        {
            source.volume -= Time.deltaTime / 5.0f;
            yield return null;
        }
        source.volume = 0;
        source.Stop();
    }

    IEnumerator fadeIn()
    {
        while (source.volume > 0.01f)
        {
            source.volume -= Time.deltaTime / 5.0f;
            yield return null;
        }
        source.volume = 0.2f;
        source.Play();
    }

}
