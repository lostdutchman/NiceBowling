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
            rigidBody.useGravity = true;
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
        print("Starting Kill");
        foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000));
        }
        yield return new WaitForSecondsRealtime(.5f);
        foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000));
        }
        yield return new WaitForSecondsRealtime(.5f);
        foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000));
        }
        yield return new WaitForSecondsRealtime(.5f);
        foreach (Rigidbody rigidBody in this.GetComponentsInChildren<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000), UnityEngine.Random.Range(-2000, 2000));
        }
    }
}
