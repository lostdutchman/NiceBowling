using UnityEngine;
using System.Collections;

public class NiceBowlingReset : MonoBehaviour
{
    public GameObject Bumper, Bumper2;
    public PhysicMaterial BowlingBall;
    public Material Ball, Skybox;
    public Camera MainCamera;

    // Use this for initialization
    void Start()
    {
        //Find values? get rid of magic numbers below where possible
        //Try to get the materials ect at the begening of the game so they dont have to be put in 2 places
    }

    //use for initilization if level is loaded more then onece in a session
    public void Reset()
    {
        //Reset Lane
        Skybox sky = MainCamera.GetComponent<Skybox>();
        sky.material = Skybox;

        Physics.gravity = new Vector3(0f, -150f, 0f);

        Bumper.SetActive(false);
        Bumper2.SetActive(false);

        //Reset Pins
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Rigidbody pinBody = pin.GetComponent<Rigidbody>();
            pinBody.angularDrag = 0.05f;
            pinBody.drag = 0f;
            pin.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Reset Ball
        Ball ball = GameObject.FindObjectOfType<Ball>();
        SphereCollider ballCol = ball.GetComponent<SphereCollider>();
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        Renderer ballRend = ball.GetComponent<Renderer>();

        ballCol.sharedMaterial = BowlingBall;
        ball.transform.localScale = new Vector3(21.7f, 21.7f, 21.7f);
        ball.transform.localPosition = new Vector3(0f, 18f, 30f); //This line should be modified to remember players last dragged location.
        ballRend.material = Ball;
        ballBody.mass = 8f;

        //Clear Obsticals tagged NBTemp
        foreach(GameObject Obstacle in GameObject.FindGameObjectsWithTag("NBTemp"))
        {
            Destroy(Obstacle);
        }
    }
}

