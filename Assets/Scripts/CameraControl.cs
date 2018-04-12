using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Ball ball;
	private const float stopCameraZ = 1750; //just before head pin
	public float stopCameraX;
	private Vector3 offset; //How far from ball should the camera be

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		offset = transform.position - ball.transform.position;
	}
	
	//Follows the ball
	void Update () {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
		if ((childBall.transform.position.z <= stopCameraZ) && (childBall.transform.position.x < stopCameraX) && (childBall.transform.position.x > (0 - stopCameraX))){
			transform.position = childBall.transform.position + offset;
		} 
	}
}
