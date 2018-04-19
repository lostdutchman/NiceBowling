using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardBall : MonoBehaviour {

    private Rigidbody rigidBody;
    public float ForceToAdd = 4;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ChildBall")
        {
            Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * ForceToAdd;
            rigidBody.AddForce(HitForce, ForceMode.Impulse);
        }
    }
}
