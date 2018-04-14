using UnityEngine;
using System.Collections;

public class NiceBowlingReset : MonoBehaviour
{
    public GameObject StandardBall;
    public float BallPosX;
    private GameObject Bumper, Bumper2;
    private PhysicMaterial BowlingBall;
    private Material Ball, SkyBoxMat;
    private Camera MainCamera;
    private Vector3 Gravity, BallSize;
    private float BallMass, BallPosY, BallPosZ;

    // Use this for initialization
    void Start()
    {
        Bumper = GameObject.FindObjectOfType<NiceBowling>().Bumper;
        Bumper2 = GameObject.FindObjectOfType<NiceBowling>().Bumper2;
        MainCamera = Camera.main;
        SkyBoxMat = MainCamera.GetComponent<Skybox>().material;
        Gravity = Physics.gravity;
        BallPosY = GameObject.FindObjectOfType<Ball>().transform.localPosition.y;
        BallPosZ = GameObject.FindObjectOfType<Ball>().transform.localPosition.z;
        BallPosX = GameObject.FindObjectOfType<Ball>().transform.localPosition.x;
    }

    //use for initilization if level is loaded more then onece in a session
    public void Reset()
    {
        //Reset Lane
        MainCamera.GetComponent<Skybox>().material = SkyBoxMat;

        Physics.gravity = Gravity;

        Bumper.SetActive(false);
        Bumper2.SetActive(false);

        //Reset Pins
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            pin.DefaultPins();
        }

        //Reset Ball
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Destroy(childBall);
        Instantiate(StandardBall, new Vector3(BallPosX, BallPosY, BallPosZ), Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);

        //Clear Obsticals tagged NBTemp
        foreach (GameObject Obstacle in GameObject.FindGameObjectsWithTag("NBTemp"))
        {
            Destroy(Obstacle);
        }
    }
}

