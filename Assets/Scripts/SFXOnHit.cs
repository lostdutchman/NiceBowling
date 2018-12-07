using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOnHit : MonoBehaviour {

    private AudioSource SoundPlayer;

    private void Start()
    {
        SoundPlayer = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ChildBall")
        SoundPlayer.Play();
    }
}
