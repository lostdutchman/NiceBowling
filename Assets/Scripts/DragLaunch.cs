using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

	private Ball ball;
	private Vector3 startPos, endPos;
	private float startTime, endTime;
	private const float ballReleseHight = 0;
	private const float minLaunchSpeed = 40;
	
	void Start () {
		ball = GetComponent<Ball>();
	}
	
	//Figures out where you started dragging to launch the ball
	public void DragStart(){
	startPos = Input.mousePosition;
	startTime = Time.time;
	}
	
	//Figures out where you stopped dragging to launch the ball
	public void DragEnd(){
	endPos = Input.mousePosition;
	endTime = Time.time;
	CalculateDrag ();
	}
	
	//Calculates the direction and speed of the ball launch using info from the previous 2 methods.
	public void CalculateDrag(){
		if(!ball.inPlay){
			float dragDuration = endTime - startTime;
			//Speed = distance (end - start) devided by time
			float launchSpeedX = (endPos.x - startPos.x) / 1.6f; //I devided it by 1.8 to keep it easier to bowl straight.
			float launchSpeedZ = (endPos.y - startPos.y) / dragDuration;

			if(launchSpeedZ < minLaunchSpeed){
				ball.Launch (new Vector3(launchSpeedX + (minLaunchSpeed/2), ballReleseHight, minLaunchSpeed));
			} else {
			ball.Launch (new Vector3(launchSpeedX, ballReleseHight, launchSpeedZ));
			}
		}
	}
	
	//This is called by the UI interface to move the ball left or right
	public void MoveStart(float xNudge){
		if(!ball.inPlay){
			float xPos = Mathf.Clamp (ball.transform.position.x + xNudge, -50f, 50f);
			float yPos = ball.transform.position.y;
			float zPos = ball.transform.position.z;
			ball.transform.position = new Vector3 (xPos, yPos, zPos);
		}
	}
}
