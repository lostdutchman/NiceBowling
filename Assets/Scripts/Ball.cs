using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public bool inPlay = false;
    public GameObject jellyBall;
    public AudioClip BallRolling;
    private AudioSource audioSource;
    public GameObject Tut, LeftArrow, RightArrow, TouchInput;
    private NiceBowlingReset NBReset;

    void Start () {
		audioSource = GetComponent<AudioSource>();
        NBReset = FindObjectOfType<NiceBowlingReset>();
    }

    public void Launch (Vector3 velocity)
	{
        audioSource.clip = BallRolling;
        audioSource.Play();
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        ThingTracker.LastStartPos = childBall.transform.position;
        NBReset.BallPosX = childBall.transform.position.x;
        if(childBall.name == "JellyBall(Clone)")
        {
            GameObject.FindObjectOfType<JellyBall>().Launch(velocity);
        }
        else
        {
            childBall.GetComponent<Rigidbody>().useGravity = true;
            childBall.GetComponent<Rigidbody>().velocity = velocity;
        }
		inPlay = true;
        Tut.SetActive(false);
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
    }
	
	public void Reset ()
	{
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Rigidbody rigidBody = childBall.GetComponent<Rigidbody>();
        GameObject.FindGameObjectWithTag("ChildBall").GetComponent<MeshRenderer>().enabled = false;
        childBall.transform.position = ThingTracker.LastStartPos;
		childBall.transform.rotation = Quaternion.identity;
        rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
        inPlay = false;
	}

    void FixedUpdate()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        //Make it so that if ball stops moving the frame resets
        if (inPlay)
        {
            if (childBall.GetComponent<Rigidbody>().velocity.z <= 0)
            {
                ThingTracker.ballout = true;
            }
        }
        else
        {
            if (TouchInput.activeInHierarchy == true)
            {
                LeftArrow.SetActive(true);
                RightArrow.SetActive(true);
                childBall.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void TryAgain()
    {
        Tut.SetActive(true);
    }

    public void ResetTheJellyBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Destroy(childBall);
        Instantiate(jellyBall, ThingTracker.LastStartPos, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
    }
}
