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
    private Quaternion rotation;
    private float lastHeight = 60;
    private int rotateQueue = 0;

	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		offset = transform.position - ball.transform.position;
        rotation = transform.rotation;
	}

    //Follows the ball
    void Update() {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 TempOffset = new Vector3(0,0,0);

        if (childBall.name == "SpikeBall 1(Clone)")
        {
            TempOffset = new Vector3(0, childBall.GetComponent<SphereCollider>().radius - .3f, (childBall.GetComponent<SphereCollider>().radius -.3f) * -1);
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

        if (transform.position.y >= 45)
        {
            TiltCameraDown(transform.position.y);
        }
        else if (transform.position.y < 45)
        {
            transform.rotation = rotation;
            lastHeight = 45;
        }

        if(rotateQueue > 0)
        {
            TiltCameraDownSmothe();
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
        yield return new WaitForSecondsRealtime(Delay);
        Billiards = false;
        Freeze = false;
    }

    public void FreezeCamera(float Delay)
    {
        Freeze = true;
        StartCoroutine(ResetCamera(Delay));
    }

    private void TiltCameraDown(float currentHeight) //Every 10 units taller the camera tilts down by 3 until it reaches 180 high and 46 tilt
    {
        if(currentHeight > lastHeight)
        {
            if(lastHeight < 175)
            {
                rotateQueue++;
                lastHeight = lastHeight + 10;
            }
        }
    }
    public void TiltCameraDownSmothe()
    {
        while(rotateQueue > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(transform.rotation.x + 3f, transform.rotation.y, transform.rotation.z, transform.rotation.w), .03f * Time.deltaTime);
            rotateQueue--;
        }
    }
}
