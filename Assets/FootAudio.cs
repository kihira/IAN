﻿using UnityEngine;
using System.Collections;

public class FootAudio: MonoBehaviour
{

    CharacterController cc;
    public AudioClip otherClip;
    [SerializeField] AudioSource footstep;
    private float lastPlayed;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && footstep.isPlaying == false && Time.time - lastPlayed > 0.5f)
        {
            footstep.volume = Random.Range(0.01f, 0.05f);
            footstep.pitch = Random.Range(0.8f, 1.1f);
            footstep.Play();
            lastPlayed = Time.time;
        }

    }
}