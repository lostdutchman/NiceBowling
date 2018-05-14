using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour {

    private Rigidbody rigidBody;
    [Tooltip("How much force is multipied on impact with ball.")]
    public float ForceToAdd = 4;
    private AudioSource ClackNoise;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        ClackNoise = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Floor")
        {
            Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * ForceToAdd;
            rigidBody.AddForce(HitForce, ForceMode.Impulse);
            ClackNoise.Play();
        }
    }
}
