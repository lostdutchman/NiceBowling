using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour {

    private Rigidbody rigidBody;
    public float Bouciness = 2;
    private AudioSource SoundPlayer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        SoundPlayer = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * Bouciness;
        rigidBody.AddForce(HitForce, ForceMode.Impulse);
        SoundPlayer.Play();
    }

}
