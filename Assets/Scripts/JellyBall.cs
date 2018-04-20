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
}
