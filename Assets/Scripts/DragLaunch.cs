using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    //Drag Launch Variables
	private Ball ball;
    private Vector3 startPos, endPos;
	private float startTime, endTime;
	private const float ballReleseHight = 0;
	private const float minLaunchSpeed = 40;
    private bool Dragging = false;
    private List<float> DragPointsX = new List<float>();

    //Arrow Variables
    [Tooltip("How fast the aim arrows should move the ball.")]
    public float aimAdjust = 3;
    private bool rightAim = false;
    private bool leftAim = false;
    public AudioClip ArrowSound;
    private AudioSource Audio;

    //Handicap Variables
    [Tooltip("1 = no handicap 1.6 = old handicap")]
    public float MakeAimEasier = 1.2f;
    [Tooltip("1 = no handicap Higher numbers slow bowl speeds")]
    public float SlowDown = 1;

    void Start () {
		ball = GetComponent<Ball>();
        Audio = ball.GetComponent<AudioSource>();
	}

    //Allow user to hold to continuasly adjust aim
    private void Update()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        if (rightAim || leftAim)
        {            
            if (!ball.inPlay)
            {
                float aimAdjustDirection = aimAdjust;
                if (leftAim)
                {
                    aimAdjustDirection = aimAdjust * -1;
                }
                float xPos = Mathf.Clamp(childBall.transform.position.x + aimAdjustDirection, -50f, 50f);
                float yPos = childBall.transform.position.y;
                float zPos = childBall.transform.position.z;
                childBall.transform.position = new Vector3(xPos, yPos, zPos);
            }

        }
        if (Dragging)
        {
            DragPointsX.Add(Input.mousePosition.x);
        }
    }



    public void onPointerDownAdjust(string direction)
    {
        Audio.clip = ArrowSound;
        Audio.Play();
        if (direction.ToLower() == "right") { rightAim = true; }
        else if (direction.ToLower() == "left") { leftAim = true; }
    }

    public void OnPointerUpAdjust()
    {
        rightAim = false;
        leftAim = false;
    }

    //Figures out where you started dragging to launch the ball
    public void DragStart(){
	    startPos = Input.mousePosition;
	    startTime = Time.time;
        Dragging = true;
	}
	
	//Figures out where you stopped dragging to launch the ball
	public void DragEnd(){
	    endPos = Input.mousePosition;
	    endTime = Time.time;
        Dragging = false;
	    CalculateDrag ();
	}

    public float CalculateCurve(float BallDirection)
    {
        float MinX = (DragPointsX.Min() - startPos.x) - BallDirection; //Gets difference between end and the furthast left the player moved
        float MaxX = BallDirection - (DragPointsX.Max() - startPos.x);//Gets difference bettween end and the furthers right the player moved
        if(MinX > MaxX)
        {
            return MinX;
        }
        else if(MaxX > MinX)
        {
            return MaxX *-1;//for some reason even though the positive direction is right the ball has to be pushed in negitive to go right...
        }
        else
        {
            return 0; //in this case they would equal eachother.
        }
    }
	
	//Calculates the direction and speed of the ball launch using info from the previous 2 methods.
	public void CalculateDrag(){
        if (!ball.inPlay){
            float AverageX = DragPointsX.Average();
            float dragDuration = endTime - startTime;
			//Speed = distance (end - start) devided by time
			float launchSpeedX = ((endPos.x - startPos.x) + (AverageX - startPos.x) / dragDuration) / MakeAimEasier; //I devided it by 1.2 to keep it easier to bowl straight.
            float Curve = CalculateCurve(launchSpeedX);
            float launchSpeedZ = ((endPos.y - startPos.y) / dragDuration) / SlowDown;
            if(launchSpeedZ > 3000) { launchSpeedZ = 2800; }
            if (launchSpeedZ < 200) { launchSpeedZ = 220; }

            DragPointsX.Clear();

            //Makes sure player bowled properly
            if (launchSpeedZ < minLaunchSpeed){
                ball.TryAgain();
			} else {
			ball.Launch (new Vector3(launchSpeedX, ballReleseHight, launchSpeedZ), Curve);
			}
		}
	}
}
