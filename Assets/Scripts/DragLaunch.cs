using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
    public float ArrowAnimationSize;
    public float ArrowStartSize;
    public bool AI, AITriggered;
    public GameObject TouchInput;

    //Handicap Variables
    [Tooltip("1 = no handicap 1.6 = old handicap")]
    public float MakeAimEasier = 1.2f;
    [Tooltip("1 = no handicap Higher numbers slow bowl speeds")]
    public float SlowDown = 1;

    void Start () {
		ball = GetComponent<Ball>();
        Audio = ball.GetComponent<AudioSource>();
        AITriggered = false;
	}

    //Allow user to hold to continuasly adjust aim
    private void Update()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        if (AI && !AITriggered)
        {
            if (TouchInput.activeSelf)
            {
                AITriggered = true;
                StartCoroutine(AILaunch(childBall));
            }
        }
        else if (rightAim || leftAim)
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

    private IEnumerator AILaunch(GameObject childBall)
    {
        if (!ball.inPlay)
        {
            //yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(.3f, 1.3f));
            //This needs to be slower to make it seem like a person.
            //float xPos = UnityEngine.Random.Range(-10, 10);
            //float yPos = childBall.transform.position.y;
            //float zPos = childBall.transform.position.z;
            //childBall.transform.position = new Vector3(xPos, yPos, zPos);
            //yield return new WaitForSecondsRealtime(1f);

            float launchSpeedX = UnityEngine.Random.Range(-20, 21);
            float launchSpeedZ = UnityEngine.Random.Range(400, 1500);
            float Curve = UnityEngine.Random.Range(-10, 11);
            //Debug.Log("X:" + launchSpeedX + " Z:" + launchSpeedZ + " C:" + Curve);
            ball.Launch(new Vector3(launchSpeedX, ballReleseHight, launchSpeedZ), Curve);
        }
        yield return new WaitForSecondsRealtime(1f);
        AITriggered = false;
    }

    public void OnPointerDownAdjust(string direction)
    {
        if (direction.ToLower() == "right") { rightAim = true; }
        else if (direction.ToLower() == "left") { leftAim = true; }
    }

    public void OnPointerDownEffect(GameObject Arrow)
    {
        Audio.clip = ArrowSound;
        Audio.Play();
        Arrow.transform.localScale = new Vector3(ArrowAnimationSize, ArrowAnimationSize, 0);
    }

    public void OnPointerUpAdjust(GameObject Arrow)
    {
        Arrow.transform.localScale = new Vector3(ArrowStartSize, ArrowStartSize, 0);
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
            return MaxX * -1;//Apply negitive force to push ball left
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
            if(launchSpeedZ > 2500) { launchSpeedZ = 2000; }
            if (launchSpeedZ < 200) { launchSpeedZ = 0; }

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
