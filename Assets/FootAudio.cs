using UnityEngine;
using System.Collections;

public class FootAudio: MonoBehaviour
{

    CharacterController cc;
    public AudioClip otherClip;
    [SerializeField] AudioSource footstep;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && footstep.isPlaying == false)
        {
            footstep.volume = Random.Range(0.1f, 0.3f);
            footstep.pitch = Random.Range(0.8f, 1.1f);
            footstep.Play();
        }

    }
}