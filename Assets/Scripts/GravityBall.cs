using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBall : MonoBehaviour {

    private CameraControl cam;
    public float CameraDelay;

    public void Start()
    {
        cam = FindObjectOfType<CameraControl>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin" || collision.gameObject.tag == "NBTemp")
        {
            cam.FreezeCamera(CameraDelay);
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
