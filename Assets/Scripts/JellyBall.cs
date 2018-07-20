using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JellyBall : MonoBehaviour
{
    public void Launch(Vector3 velocity)
    {
        foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity = velocity;
        }
    }

    public void kill()
    {
        StartCoroutine(WaitAndKill());
    }

    private IEnumerator WaitAndKill()
    {
        yield return new WaitForSecondsRealtime(1f);
        try
        {
            //Gotta kill the jelly ball
            foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000));
            }
            kill();
        }
        catch
        {
            //Jelly ball is dead
            yield break;
        }

    }
}
