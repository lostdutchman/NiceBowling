using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendHit : MonoBehaviour {

    [Tooltip("How much force is multipied on impact with ball.")]
    public float ForceToAdd = 15;
    private AudioSource HitNoise;

    private void Start()
    {
        HitNoise = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ChildBall" || col.gameObject.tag =="NBTemp")
        {
            Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * ForceToAdd;
            try
            {
                col.gameObject.GetComponent<Rigidbody>().AddForce(HitForce, ForceMode.Impulse);
            }
            catch
            {
                try
                {
                    foreach(Rigidbody Body in col.gameObject.GetComponentsInChildren<Rigidbody>())
                    {
                        Body.AddForce(HitForce, ForceMode.Impulse);
                    }
                }
                catch
                {
                    //No ridged body, do nothing.
                }
            }
            HitNoise.Play();
        }
    }
}
