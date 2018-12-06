using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendHit : MonoBehaviour {

    [Tooltip("How much force is multipied on impact with ball.")]
    public float ForceToAdd = 15;
    public AudioClip BallHitNoise, HitNoise;
    private AudioSource SoundPlayer;

    private void Start()
    {
        SoundPlayer = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "SpikeBall 1(Clone)")
        {

            if (col.gameObject.tag == "ChildBall" || col.gameObject.tag == "NBTemp")
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
                        foreach (Rigidbody Body in col.gameObject.GetComponentsInChildren<Rigidbody>())
                        {
                            Body.AddForce(HitForce, ForceMode.Impulse);
                        }
                    }
                    catch
                    {
                        //No ridged body, do nothing.
                    }
                }
                if (col.gameObject.tag == "ChildBall") { SoundPlayer.clip = BallHitNoise; SoundPlayer.Play(); }
                else { SoundPlayer.clip = HitNoise; SoundPlayer.Play(); }
            }
        }
    }
}
