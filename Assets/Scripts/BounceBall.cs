using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour {

    private Rigidbody rigidBody;
    public float Bouciness = 2;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * Bouciness;
        rigidBody.AddForce(HitForce, ForceMode.Impulse);
    }

}
