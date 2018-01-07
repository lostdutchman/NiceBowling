using UnityEngine;
using System.Collections;

public class NiceBowlingReset : MonoBehaviour
{
    private GameObject Bumper, Bumper2;
    private PhysicMaterial BowlingBall;
    private Material Ball, SkyBoxMat;
    private Camera MainCamera;
    private Vector3 Gravity, PinSize, BallSize;
    private float PinDrag, PinAngularDrag, BallMass, BallPosY, BallPosZ;

    // Use this for initialization
    void Start()
    {
        Bumper = GameObject.FindObjectOfType<NiceBowling>().Bumper;
        Bumper2 = GameObject.FindObjectOfType<NiceBowling>().Bumper2;
        MainCamera = Camera.main;
        BowlingBall = GameObject.FindObjectOfType<Ball>().GetComponent<SphereCollider>().material;
        Ball = GameObject.FindObjectOfType<Ball>().GetComponent<Renderer>().material;
        SkyBoxMat = MainCamera.GetComponent<Skybox>().material;
        Gravity = Physics.gravity;
        PinSize = GameObject.FindGameObjectWithTag("Pin").transform.localScale;
        PinDrag = GameObject.FindGameObjectWithTag("Pin").GetComponent<Rigidbody>().drag;
        PinAngularDrag = GameObject.FindGameObjectWithTag("Pin").GetComponent<Rigidbody>().angularDrag;
        BallSize = GameObject.FindObjectOfType<Ball>().transform.localScale;
        BallPosY = GameObject.FindObjectOfType<Ball>().transform.localPosition.y;
        BallPosZ = GameObject.FindObjectOfType<Ball>().transform.localPosition.z;
        BallMass = GameObject.FindObjectOfType<Ball>().GetComponent<Rigidbody>().mass;
    }

    //use for initilization if level is loaded more then onece in a session
    public void Reset()
    {
        //This allows the possition the player set to remain between frames. 
        float BallPosX = GameObject.FindObjectOfType<Ball>().transform.localPosition.x;

        //Reset Lane
        MainCamera.GetComponent<Skybox>().material = SkyBoxMat;

        Physics.gravity = Gravity;

        Bumper.SetActive(false);
        Bumper2.SetActive(false);

        //Reset Pins
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Rigidbody pinBody = pin.GetComponent<Rigidbody>();
            pinBody.angularDrag = PinAngularDrag;
            pinBody.drag = PinDrag;
            pin.transform.localScale = PinSize;
        }

        //Reset Ball
        Ball ball = GameObject.FindObjectOfType<Ball>();
        SphereCollider ballCol = ball.GetComponent<SphereCollider>();
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        Renderer ballRend = ball.GetComponent<Renderer>();

        ballCol.sharedMaterial = BowlingBall;
        ball.transform.localScale = BallSize;
        ball.transform.localPosition = new Vector3(BallPosX, BallPosY, BallPosZ); 
        ballRend.material = Ball;
        ballBody.mass = BallMass;

        //Clear Obsticals tagged NBTemp
        foreach(GameObject Obstacle in GameObject.FindGameObjectsWithTag("NBTemp"))
        {
            Destroy(Obstacle);
        }
    }
}

