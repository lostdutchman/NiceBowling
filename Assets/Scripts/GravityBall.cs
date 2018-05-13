using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin" || collision.gameObject.tag == "NBTemp")
        {
            try
            {
                collision.rigidbody.useGravity = false;
                GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2000, 2000), Random.Range(-2000, 2000), Random.Range(-2000, 2000));

            }
            catch
            {
                GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2000, 2000), Random.Range(-2000, 2000), Random.Range(-2000, 2000));
            }
        }
    }
}
