using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Ball ball;
	private const float stopCameraZ = 1750; //just before head pin
    public float stopCameraX;
	private Vector3 offset; //How far from ball should the camera be
    private bool Billiards = false;
    private bool Freeze = false;
    private GameObject billiardBall;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		offset = transform.position - ball.transform.position;
        print(offset);
	}

    //Follows the ball
    void Update() {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 TempOffset = new Vector3(0,0,0);

        if (childBall.name == "SpikeBall 1(Clone)")
        {
            print(childBall.GetComponent<SphereCollider>().radius);
            TempOffset = new Vector3(0, childBall.GetComponent<SphereCollider>().radius - .5f, childBall.GetComponent<SphereCollider>().radius - .5f);
        }
        else
        {
            TempOffset = new Vector3(0, 0, 0);
        }

        if (Billiards){
            if ((billiardBall.transform.position.z <= stopCameraZ) && (billiardBall.transform.position.x < stopCameraX) && (billiardBall.transform.position.x > (0 - stopCameraX)))
            {
                transform.position = billiardBall.transform.position + offset;
            }

        }
        else if ((childBall.transform.position.z <= stopCameraZ) && (childBall.transform.position.x < stopCameraX) && (childBall.transform.position.x > (0 - stopCameraX)) && !Freeze){
			transform.position = childBall.transform.position + offset + TempOffset;
		} 
	}

    public void BilliardHit(GameObject ball, float Delay)
    {
        billiardBall = ball;
        Billiards = true;
        StartCoroutine(ResetCamera(Delay));
    }

    public IEnumerator ResetCamera(float Delay)
    {
        print("Camera resetting");
        yield return new WaitForSecondsRealtime(Delay);
        Billiards = false;
        Freeze = false;
        print("Camera reset");
    }

    public void FreezeCamera(float Delay)
    {
        Freeze = true;
        StartCoroutine(ResetCamera(Delay));
    }
}
