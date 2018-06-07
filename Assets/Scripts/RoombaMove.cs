using UnityEngine;
using System.Collections;

public class RoombaMove : MonoBehaviour {

    public float speed = 10f;
    public float forwardSpeed = 2f;
    private bool rotate = false;
    private bool ignore = false;

    // Update is called once per frame
    void Update () {
        if ((transform.position.x > 40 || transform.position.x < -40) && !ignore)
        {
            StartCoroutine(TurnAround());
        }
        if(rotate)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;
        }
	
	}

    IEnumerator TurnAround()
    {
        ignore = true;
        rotate = true;
        yield return new WaitForSecondsRealtime(Random.Range(.3f, 2f));
        rotate = false;
        yield return new WaitForSecondsRealtime(Random.Range(.5f, 1f));
        ignore = false;
    }
}