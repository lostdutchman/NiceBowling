using UnityEngine;
using System.Collections;

public class RoombaMove : MonoBehaviour {

    public float speed = 10f;
    public float forwardSpeed = 2f;
    Rigidbody body;

    void Start()
    {
        body = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            //body.AddForce(transform.forward * forwardSpeed);
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;
        }
        else
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
	
	}
}