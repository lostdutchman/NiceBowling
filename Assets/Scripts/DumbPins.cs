﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbPins : MonoBehaviour {

    public AudioClip PinHitBall;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ChildBall")
        {
            audioSource.clip = PinHitBall;
            audioSource.Play();
        }
    }
}
