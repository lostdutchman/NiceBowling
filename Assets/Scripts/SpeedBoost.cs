using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    public float SpeedMultiplier;
    public AudioSource SFX;

    private void OnTriggerEnter(Collider other)
    {
        GameObject ThingEnter = other.gameObject;
        try
        {
            Rigidbody Body = ThingEnter.GetComponent<Rigidbody>();
            if (Body.velocity.z < 2000)
            {
                Body.AddForce(new Vector3(0, 0, Body.velocity.z * SpeedMultiplier), ForceMode.VelocityChange);
            }
            SFX.Play();
        }
        catch
        {
            //No ridgid body, dont apply force.
        }
    }

}
