using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Ball ball;
	private const float stopCameraZ = 1750; //just before head pin
    public float stopCameraX;
	private Vector3 offset; //How far from ball should the camera be
    private bool Billiards = false;
    private GameObject billiardBall;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		offset = transform.position - ball.transform.position;
	}

    //Follows the ball
    void Update() {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        if (Billiards){
            if ((billiardBall.transform.position.z <= stopCameraZ) && (billiardBall.transform.position.x < stopCameraX) && (billiardBall.transform.position.x > (0 - stopCameraX)))
            {
                transform.position = billiardBall.transform.position + offset;
            }

        }
        else if ((childBall.transform.position.z <= stopCameraZ) && (childBall.transform.position.x < stopCameraX) && (childBall.transform.position.x > (0 - stopCameraX))){
			transform.position = childBall.transform.position + offset;
		} 
	}

    public void BilliardHit(GameObject ball)
    {
        billiardBall = ball;
        Billiards = true;
    }

    public void ResetCamera()
    {
        Billiards = false;
    }
}
